using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class InvalidLevelSafe : MonoBehaviour
{
   [SerializeField] private List<InvalidLevelSafe> invalidLevels;
   private string ScriptableObjectspath = "Assets/Prefabs/Levels/InvalidLevels";
   private int chunkSize, chunkSizeZ, seed;

   public bool clearInvalidLevels;
   public bool createInvalidLevels;

   private void Awake()
   {
       // int assetCount = CountAssetsInFolder(ScriptableObjectspath);
        //Debug.Log("Number of assets in folder: " + assetCount);
        
        invalidLevels = new List<InvalidLevelSafe>();
        ScriptableInvalidLevel[] loadedObjects = Resources.LoadAll<ScriptableInvalidLevel>("D:/Repos/GameProgramming/ExoPrototype2/Assets/Prefabs");
        Debug.Log(loadedObjects.Length);
        foreach (var guid in loadedObjects)
        {
             InvalidLevelSafe invalidLevelSafe = new InvalidLevelSafe(guid.chunkSize, guid.chunkSizeZ, guid.seed);
             invalidLevels.Add(invalidLevelSafe);
        }
   }

   private int CountAssetsInFolder(string path)
   {
        int count = 0;
        string[] assetGuids = AssetDatabase.FindAssets("", new string[] { path });

        foreach (string guid in assetGuids)
        {
             string assetPath = AssetDatabase.GUIDToAssetPath(guid);
             if (!AssetDatabase.IsValidFolder(assetPath)) // Exclude folders
             {
                  count++;
             }
        }

        return count;
   }
   
   //Constructor for InvalidLevel
   //Contructor for Level
   public InvalidLevelSafe(int chunkSize, int chunkSizeZ, int seed)
   {
        this.chunkSize = chunkSize;
        this.chunkSizeZ = chunkSizeZ;
        this.seed = seed;
   }

   public void CreateInvalidLevelObj()
   {
        //Create new Scriptable Object of ScriptableInvalidLevel
        ScriptableInvalidLevel invalidLevel = ScriptableObject.CreateInstance<ScriptableInvalidLevel>();
        invalidLevel.chunkSize = GetComponent<PerlinNoiseGen>().chunkSize;
        invalidLevel.chunkSizeZ = GetComponent<PerlinNoiseGen>().chunkSizeZ;
        invalidLevel.seed = GetComponent<PerlinNoiseGen>().seed;
        //safe Scriptable Object to Assets Folder

        string path = "Assets/Prefabs/Levels/InvalidLevels/level.asset"; // Set your desired path
        AssetDatabase.CreateAsset(invalidLevel, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        invalidLevels.Add(new InvalidLevelSafe(GetComponent<PerlinNoiseGen>().chunkSize, GetComponent<PerlinNoiseGen>().chunkSizeZ, GetComponent<PerlinNoiseGen>().seed));
   }
   
   //Check if Data equals an Invalid Level
    public bool Equals(int chunkSizeGen, int chunkSizeZGen, int seedGen)
    {
         //Check if given data is equal any of the invalid levels in the list
           foreach (var invalidLevel in invalidLevels)
           {
                 if (invalidLevel.chunkSize == chunkSizeGen && invalidLevel.chunkSizeZ == chunkSizeZGen && invalidLevel.seed == seedGen)
                 {
                       return true;
                 }
           }

           return false;
    }

    private void Update()
    {
         if (clearInvalidLevels)
         {
              invalidLevels.Clear();
         }

         if (createInvalidLevels)
         {
              CreateInvalidLevelObj();
         }
    }
    
}
