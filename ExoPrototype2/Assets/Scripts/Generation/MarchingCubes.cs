using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes : MonoBehaviour
    {
        
        public MeshFilter meshFilter; // Reference to the MeshFilter component on the same GameObject
        public Material material;
        private Mesh generatedMesh;
        
        public MarchingCubes(MeshFilter meshFilter)
        {
            this.meshFilter = meshFilter;
        }
    // The Marching Cubes algorithm to generate a smooth voxel surface
    public void Generate(float[,,] voxelData, float threshold)
    {
        // Clear any previous mesh data
        //meshFilter.mesh.Clear();
        //generatedMesh.Clear();

        // Create lists to hold the vertex positions, triangles, and normals
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> normals = new List<Vector3>();

        int width = voxelData.GetLength(0);
        int height = voxelData.GetLength(1);
        int depth = voxelData.GetLength(2);

        // Loop through each voxel in the voxel data
        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                for (int z = 0; z < depth - 1; z++)
                {
                    // Get the eight corners of the voxel
                    float[] corners = new float[8];
                    corners[0] = voxelData[x, y, z];
                    corners[1] = voxelData[x + 1, y, z];
                    corners[2] = voxelData[x + 1, y, z + 1];
                    corners[3] = voxelData[x, y, z + 1];
                    corners[4] = voxelData[x, y + 1, z];
                    corners[5] = voxelData[x + 1, y + 1, z];
                    corners[6] = voxelData[x + 1, y + 1, z + 1];
                    corners[7] = voxelData[x, y + 1, z + 1];

                    // Calculate the voxel configuration index based on the threshold
                    int voxelIndex = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (corners[i] >= threshold)
                            voxelIndex |= 1 << i;
                    }

                    // Check if the voxel is completely outside or inside the surface
                    if (voxelIndex == 0 || voxelIndex == 255)
                        continue;

                    // Get the edge points using the edge table
                    Vector3[] edgePoints = new Vector3[12];
                    for (int i = 0; i < 12; i++)
                    {
                        if ((MarchingCubesTables.edgeConnections[voxelIndex][0] & (1 << i)) != 0)
                            edgePoints[i] = VertexInterp(MarchingCubesTables.cubeCorners[i], MarchingCubesTables.cubeCorners[i + 1], corners[i], corners[i + 1], threshold);
                        else if ((MarchingCubesTables.edgeConnections[voxelIndex][1] & (1 << i)) != 0)
                            edgePoints[i] = VertexInterp(MarchingCubesTables.cubeCorners[i + 1], MarchingCubesTables.cubeCorners[i], corners[i + 1], corners[i], threshold);
                    }

                    // Add triangles to the mesh using the triangle table
                    for (int i = 0; i < 16; i += 3)
                    {
                        if (MarchingCubesTables.triTable[voxelIndex][i] < 0)
                            break;

                        int vertexIndex1 = MarchingCubesTables.triTable[voxelIndex][i];
                        int vertexIndex2 = MarchingCubesTables.triTable[voxelIndex][i + 1];
                        int vertexIndex3 = MarchingCubesTables.triTable[voxelIndex][i + 2];

                        triangles.Add(vertices.Count);
                        vertices.Add(edgePoints[vertexIndex1]);
                        normals.Add(CalculateNormal(edgePoints[vertexIndex1], voxelData, x, y, z));

                        triangles.Add(vertices.Count);
                        vertices.Add(edgePoints[vertexIndex2]);
                        normals.Add(CalculateNormal(edgePoints[vertexIndex2], voxelData, x, y, z));

                        triangles.Add(vertices.Count);
                        vertices.Add(edgePoints[vertexIndex3]);
                        normals.Add(CalculateNormal(edgePoints[vertexIndex3], voxelData, x, y, z));
                    }
                }
            }
        }

        // Create a new mesh and set the data
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();

        // Assign the mesh to the MeshFilter component and set the material
        meshFilter.mesh = mesh;
        meshFilter.gameObject.GetComponent<MeshRenderer>().sharedMaterial = material;
        generatedMesh = CreateMesh(vertices.ToArray(), triangles.ToArray(), normals.ToArray());
        meshFilter.mesh = generatedMesh;
    }
    
    private Mesh CreateMesh(Vector3[] vertices, int[] triangles, Vector3[] normals)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        return mesh; 
    }

    // Public method to retrieve the generated mesh
    public Mesh GetGeneratedMesh()
    {
        return generatedMesh;
    }

    // Helper function to interpolate the vertex position
    private Vector3 VertexInterp(Vector3 p1, Vector3 p2, float val1, float val2, float threshold)
    {
        float t = (threshold - val1) / (val2 - val1);
        return p1 + t * (p2 - p1);
    }

    // Helper function to calculate the surface normal at a vertex position
    private Vector3 CalculateNormal(Vector3 vertexPos, float[,,] voxelData, int x, int y, int z)
    {
        float dx = voxelData[x + 1, y, z] - voxelData[x - 1, y, z];
        float dy = voxelData[x, y + 1, z] - voxelData[x, y - 1, z];
        float dz = voxelData[x, y, z + 1] - voxelData[x, y, z - 1];
        return new Vector3(dx, dy, dz).normalized;
    }
    
    }


