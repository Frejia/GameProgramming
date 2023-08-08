using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshCreator : MonoBehaviour
{
    public Material material; // Set this in the Inspector
    public bool collider = false; // Set this in the Inspector

    private List<Mesh> meshes = new List<Mesh>();

    // ... Existing code ...

    public void CreatePrefab(Transform meshContainer, Mesh mesh, string prefabName)
    {
        GameObject prefab = new GameObject(prefabName);
        prefab.transform.position = meshContainer.position;
        prefab.transform.rotation = meshContainer.rotation;
        prefab.transform.localScale = meshContainer.localScale;

        MeshFilter prefabMF = prefab.AddComponent<MeshFilter>();
        MeshRenderer prefabMR = prefab.AddComponent<MeshRenderer>();
        prefabMR.material = material;
        prefabMF.mesh = mesh;

        if (collider)
        {
            MeshCollider prefabCollider = prefab.AddComponent<MeshCollider>();
            prefabCollider.sharedMesh = prefabMF.sharedMesh;
        }

        // Save the prefab
        string prefabPath = "Assets/Prefabs/Levels" + prefabName + ".prefab";
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);

        // Destroy the temporary game object
        Destroy(prefab);

        Debug.Log("Prefab saved: " + prefabPath);
    }

    public void GenerateMeshPrefabs(List<List<CombineInstance>> blockDataLists)
    {
        Transform container = new GameObject("Meshys").transform;

        for (int i = 0; i < blockDataLists.Count; i++)
        {
            List<CombineInstance> data = blockDataLists[i];
            string prefabName = "MeshyPrefab" + i;

            GameObject g = new GameObject("Meshy");
            g.transform.parent = container;

            MeshFilter mf = g.AddComponent<MeshFilter>();
            MeshRenderer mr = g.AddComponent<MeshRenderer>();
            mr.material = material;
            mr.transform.localScale = new Vector3(3, 3, 3);
            mf.mesh.CombineMeshes(data.ToArray());
            mf.gameObject.layer = 3;

            meshes.Add(mf.mesh);

            if (collider)
            {
                g.AddComponent<MeshCollider>().sharedMesh = mf.sharedMesh;
            }
            
            int j = new System.Random().Next(0, 1000);
            CreatePrefab(g.transform, mf.sharedMesh, prefabName + j.ToString());

            Destroy(g);
        }
    }
}
