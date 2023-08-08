using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshCreator : MonoBehaviour
{
    // https://unitycoder.com/blog/2013/01/26/save-mesh-created-by-script-in-editor-playmode/
    public KeyCode saveKey = KeyCode.F12;
    public bool saveOnStart = false;
    public string saveName = "Meshys";
    [SerializeField] public Transform selectedGameObject;

    void Update()
    {
        if (saveOnStart)
        {
            Debug.Log("Pressed to Save");
            MakePrefab();
        }
    }

    void SaveAsset(int numofmesh)
    {
        // Get How many children are in the selectedGameObject
        int childCount = selectedGameObject.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var mf = selectedGameObject.transform.GetChild(i).GetComponent<MeshFilter>();
            if (mf)
            {
                //Create new Mesh Folder
                    string folderPath = AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder("Assets/Prefabs/Levels/", "Meshes" + numofmesh));
                    var savePath = folderPath+ "/" + saveName + i + ".asset";
                    Debug.Log("Saved Mesh to:" + savePath);
                    AssetDatabase.CreateAsset(mf.mesh, savePath);
            }
        }
    }
    
    public void MakePrefab()
    {
        selectedGameObject = GameObject.Find("Meshys").transform;
        GameObject mesh = selectedGameObject.gameObject;
        int i = Random.Range(0, 1000);
        SaveAsset(i);
        //If Prefab already exists, do not create it again
        if (AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Levels/" + mesh.name + i + ".prefab", typeof(GameObject)))
        {
            Debug.Log("Prefab already exists");
            return;
        }
        else
        {
            // Create a prefab at the specified path
            string prefabPath = "Assets/Prefabs/Levels/" + mesh.name + i + ".prefab";
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(mesh, prefabPath);
            if (prefab != null)
            {
                Debug.Log("Prefab created at: " + prefabPath);
            }
            else
            {
                Debug.LogError("Failed to create prefab.");
            }
        }
        

       
    }
}