using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class PerlinNoiseGen : MonoBehaviour
{

    [SerializeField] public bool raceMode = false;
    [SerializeField] public float pathradius = 10f;
    
    [SerializeField] private int seed = 0;
    [SerializeField] private GameObject blockPrefab;//use a unit cube (1x1x1 like unity's default cube)
    [SerializeField] private int chunkSize = 50;
    [SerializeField] private int chunkSizeZ = 50;
    [SerializeField] private float noiseScale = .05f;
    [SerializeField, Range(0, 1)] private float threshold = .5f;
    [SerializeField] private Material material;
    [SerializeField] private bool sphere = false;
    [SerializeField] private bool collider = false;
    private List<Mesh> meshes = new List<Mesh>();//used to avoid memory issues
    [SerializeField] private List<Vector3> interpolatedPoints = new List<Vector3>();
    // The reference to the MarchingCubes script
    private MarchingCubes marchingCubes;
    [SerializeField] public List<GameObject> waypoints;
    private float timePassed = 0f;

    public static PerlinNoiseGen Instance { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(chunkSize, chunkSize, chunkSizeZ));
        // Draw Wire Cube in Red
        
    }

    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Get the reference to the MarchingCubes script on the same GameObject
        marchingCubes = GetComponent<MarchingCubes>();
    }

    private List<CombineInstance> blockData;//this will contain the data for the final mesh

    private List<List<CombineInstance>> blockDataLists;
    
    // Start is called before the first frame update
    public void Generate() {
        float startTime = Time.realtimeSinceStartup;

        #region Create Mesh Data

        blockData = new List<CombineInstance>();//this will contain the data for the final mesh
        MeshFilter blockMesh = Instantiate(blockPrefab, transform.position, Quaternion.identity).GetComponent<MeshFilter>();//create a unit cube and store the mesh from it
        
        // Convert the block data to a 3D float array for MarchingCubes
        float[,,] noiseMap = new float[chunkSize, chunkSize, chunkSizeZ];
        
        //go through each block position
        for (int x = 0; x < chunkSize; x++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int z = 0; z < chunkSizeZ; z++) {

                    float noiseValue = Perlin3D((x + seed) * noiseScale, (y + seed) * noiseScale, (z + seed) * noiseScale);//get value of the noise at given x, y, and z.
                    noiseMap[x, y, z] = noiseValue;
                    if (noiseValue >= threshold) {//is noise value above the threshold for placing a block?

                        //ignore this block if it's a sphere and it's outside of the radius (ex: in the corner of the chunk, outside of the sphere)
                        //distance between the current point with the center point. if it's larger than the radius, then it's not inside the sphere.
                        float radius = chunkSize / 2;
                        if (sphere && Vector3.Distance(new Vector3(x, y, z), Vector3.one * radius) > radius)
                            continue;

                        blockMesh.transform.position = new Vector3(x, y, z);//move the unit cube to the intended position
                        CombineInstance ci = new CombineInstance {//copy the data off of the unit cube
                            mesh = blockMesh.sharedMesh,
                            transform = blockMesh.transform.localToWorldMatrix,
                        };
                        blockData.Add(ci);//add the data to the list
                    }

                }
            }
        }
        if (raceMode)
        {
            Debug.Log("Gen Race Mode");
            GenerateBezierPoints(waypoints[0].transform.position, waypoints[waypoints.Count-1].transform.position, 10);
            /*foreach (GameObject points in waypoints)
            {
                interpolatedPoints.Add(points.transform.position);
            }*/
            RemoveCubesWithinRadius(interpolatedPoints, pathradius);
        }
        
        Destroy(blockMesh.gameObject);//original unit cube is no longer needed. we copied all the data we need to the block list.

        #endregion

        #region Separate Mesh Data

        //divide meshes into groups of 65536 vertices. Meshes can only have 65536 vertices so we need to divide them up into multiple block lists.

        blockDataLists = new List<List<CombineInstance>>();//we will store the meshes in a list of lists. each sub-list will contain the data for one mesh. same data as blockData, different format.
        int vertexCount = 0;
        blockDataLists.Add(new List<CombineInstance>());//initial list of mesh data
        for (int i = 0; i < blockData.Count; i++) {//go through each element in the previous list and add it to the new list.
            vertexCount += blockData[i].mesh.vertexCount;//keep track of total vertices
            if (vertexCount > 65536) {//if the list has reached it's capacity. if total vertex count is more then 65536, reset counter and start adding them to a new list.
                vertexCount = 0;
                blockDataLists.Add(new List<CombineInstance>());
                i--;
            } else {//if the list hasn't yet reached it's capacity. safe to add another block data to this list 
                blockDataLists.Last().Add(blockData[i]);//the newest list will always be the last one added
            }
        }

        #endregion

        #region Create Mesh

        //the creation of the final mesh from the data.

        Transform container = new GameObject("Meshys").transform;//create container object
        foreach (List<CombineInstance> data in blockDataLists) {//for each list (of block data) in the list (of other lists)
            GameObject g = new GameObject("Meshy");//create gameobject for the mesh
            g.transform.parent = container;//set parent to the container we just made
            MeshFilter mf = g.AddComponent<MeshFilter>();//add mesh component
            MeshRenderer mr = g.AddComponent<MeshRenderer>();//add mesh renderer component
            mr.material = material;//set material to avoid evil pinkness of missing texture
            mr.transform.localScale = new Vector3(3,3,3);//scale up the mesh so it's visible
            mf.mesh.CombineMeshes(data.ToArray());//set mesh to the combination of all of the blocks in the list
            mf.GameObject().layer = "Terrain".GetHashCode();//set layer to "Terrain
            meshes.Add(mf.mesh);//keep track of mesh so we can destroy it when it's no longer needed
            if(collider) g.AddComponent<MeshCollider>().sharedMesh = mf.sharedMesh;//setting colliders takes more time. disabled for testing.
        }

        #endregion

        //Debug.Log("Loaded in " + (Time.realtimeSinceStartup - startTime) + " Seconds.");
        
        Debug.Log("Generated Mesh in " + timePassed + "seconds.");

    }
    
    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Destroy(GameObject.Find("GeneratedMesh"));//destroy the previously generated mesh.
            foreach (Mesh m in meshes)//meshes still exist even though they aren't in the scene anymore. destroy them so they don't take up memory.
                Destroy(m);
            
            // Generate the voxel data using Perlin noise
           // float[,,] voxelData = GenerateVoxelData();

            // Create the mesh using Marching Cubes
           // CreateMarchingCubesMesh(voxelData);
        }
        
        timePassed += Time.deltaTime;
        
    }

    // Method to generate points along a Bezier curve between start, control, and end points.
    private void GenerateBezierPoints(Vector3 startPoint, Vector3 endPoint, int segments)
    {
        Debug.Log("Generate Bezier Points");
        //interpolatedPoints = new List<Vector3>();
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            Vector3 point = Vector3.Lerp(startPoint, endPoint, t);
            //var point = Mathf.Pow(1 - t, 2) * startPoint + 2 * t * (1 - t) * controlPoint + Mathf.Pow(t, 2) * endPoint;
            interpolatedPoints.Add(point);
        }
        
        Debug.Log("GenerateBezPoints Finished after " + timePassed + " seconds.");
    }

    private void RemoveCubesWithinRadius(List<Vector3> points, float radius)
    {
        Debug.Log("Start Removing Cubes");
        for (int i = blockData.Count - 1; i >= 0; i--)
        {
            Vector3 cubePosition = blockData[i].transform.MultiplyPoint(Vector3.zero); // Get the position of the cube in world space.

            foreach (Vector3 point in points)
            {
                if (Vector3.Distance(cubePosition, point) < radius)
                {
                    Destroy(blockData[i].mesh.GameObject()); // Destroy the cube game object directly.
                    blockData.RemoveAt(i);
                    break; // Remove the cube and move on to the next one.
                }
            }
        }
        Debug.Log("Finished Removing Cubes after " + timePassed + " seconds.");
    }
    
    public static float Perlin3D(float x, float y, float z)
    {
        float ab = Mathf.PerlinNoise(x, y);
        float bc = Mathf.PerlinNoise(y, z);
        float ac = Mathf.PerlinNoise(x, z);
        
        float ba = Mathf.PerlinNoise(y, x);
        float cb = Mathf.PerlinNoise(z, y);
        float ca = Mathf.PerlinNoise(z, x);
        
        float abc = ab + bc + ac + ba + cb + ca;
        return abc / 6f;
    }
}
