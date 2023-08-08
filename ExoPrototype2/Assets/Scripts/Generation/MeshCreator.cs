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

    private void CreatePrefab(Mesh combinedMesh, string prefabName)
    {
        GameObject prefab = new GameObject(prefabName);
        MeshFilter prefabMF = prefab.AddComponent<MeshFilter>();
        MeshRenderer prefabMR = prefab.AddComponent<MeshRenderer>();
        prefabMR.material = material;
        prefabMF.mesh = combinedMesh;

        if (collider)
        {
            MeshCollider prefabCollider = prefab.AddComponent<MeshCollider>();
            prefabCollider.sharedMesh = prefabMF.sharedMesh;
        }

#if UNITY_EDITOR
        // Save the prefab
        string prefabPath = "Assets/Prefabs/" + prefabName + ".prefab";
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
        Debug.Log("Prefab saved: " + prefabPath);
#endif

        Destroy(prefab);
    }

    public void GenerateMeshPrefab(List<List<CombineInstance>> blockDataLists)
    {
        Transform container = new GameObject("Meshys").transform;

        List<CombineInstance> finalData = new List<CombineInstance>();

        foreach (List<CombineInstance> data in blockDataLists)
        {
            finalData.AddRange(data);
        }

        GameObject g = new GameObject("CombinedMesh");
        g.transform.parent = container;

        MeshFilter mf = g.AddComponent<MeshFilter>();
        MeshRenderer mr = g.AddComponent<MeshRenderer>();
        mr.material = material;
        mr.transform.localScale = new Vector3(3, 3, 3);
        mf.mesh.CombineMeshes(finalData.ToArray());
        mf.gameObject.layer = 3;

        meshes.Add(mf.mesh);

        if (collider)
        {
            g.AddComponent<MeshCollider>().sharedMesh = mf.sharedMesh;
        }

        CreatePrefab(mf.sharedMesh, "CombinedMeshPrefab");

        Destroy(g);
    }
}