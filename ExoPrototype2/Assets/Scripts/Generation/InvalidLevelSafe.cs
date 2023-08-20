using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// If a level is not playable due to the generation, the data of the level is saved in a Scriptable Object
/// Level is then checked during PerlinNoiseGeneration so it is not generated again
///
/// Originally made with UnityEditor intern methods, which only worked in the Editor, changed later to work in Build
/// </summary>
public class InvalidLevelSafe : MonoBehaviour
{
     [SerializeField] private List<InvalidLevelSafe> invalidLevels;
     public static InvalidLevelSafe Instance;
     [SerializeField] private ScriptableInvalidLevel invalidLevel; // Reference to the Scriptable Object template
     
     // Path to the folder where the Scriptable Objects are saved
   private string ScriptableObjectspath = "Assets/Prefabs/Levels/InvalidLevels";
   private int chunkSize, chunkSizeZ, seed;

   // Debug Variables
   public bool clearInvalidLevels;
   public bool createInvalidLevels;
   
   
   private void Awake()
   {
        Instance = this;
   }

   // Edit the Scriptable Object to add a new Invalid Level
   public void EditInvalidLevelSave()
   {
        // Set the values of the Scriptable Object
        invalidLevel.chunkSize.Add(GetComponent<PerlinNoiseGen>().chunkSize);
        invalidLevel.chunkSizeZ.Add(GetComponent<PerlinNoiseGen>().chunkSizeZ);
        invalidLevel.offset.Add(GetComponent<PerlinNoiseGen>().offset);

        string filePath = Path.Combine(Application.persistentDataPath, "InvalidLevel.asset");
        SaveScriptableObject(invalidLevel, filePath);
   }

   // Save the changed Scriptable Object
   private void SaveScriptableObject(ScriptableInvalidLevel level, string filePath)
   {
        string json = JsonUtility.ToJson(level);
        File.WriteAllText(filePath, json);
   }
   
   // Compare the values from the Scriptable Object and the Current PerlinNoiseGenerator
   public bool LoadAndCompareCustomData()
   {
        bool invalid = false;
        
        string filePath = Path.Combine(Application.persistentDataPath, "InvalidLevel.asset");
        if (File.Exists(filePath))
        {
             string json = File.ReadAllText(filePath);
             ScriptableInvalidLevel loadedLevel = JsonUtility.FromJson<ScriptableInvalidLevel>(json);

             // Compare the values from the loaded Scriptable Object
             for (int i = 0; i < loadedLevel.chunkSize.Count; i++)
             {
                  int loadedChunkSize = loadedLevel.chunkSize[i];
                  int loadedChunkSizeZ = loadedLevel.chunkSizeZ[i];
                  int loadedOffset = loadedLevel.offset[i];

                  int currentChunkSize = GetComponent<PerlinNoiseGen>().chunkSize;
                  int currentChunkSizeZ = GetComponent<PerlinNoiseGen>().chunkSizeZ;
                  int currentOffset = GetComponent<PerlinNoiseGen>().offset;

                  if (loadedChunkSize == currentChunkSize && loadedChunkSizeZ == currentChunkSizeZ && loadedOffset == currentOffset)
                  {
                       invalid = true;
                       return invalid;
                  }
                  else
                  {
                       invalid = false;
                  }
             }
        }
        else
        {
             invalid = false;
             return invalid;
        }

        return invalid;
   }
   
   
   /// <summary>
   /// ----- LEGACY: Loads the Scriptable Objects from the folder and read the Invalid Levels --> Only worked in Unity Editor
   /// </summary>
#if UNITY_EDITOR
   // Former Awake Method, loaded all Invalid Level Objects
   private void GetInvalidLevels()
   {
        // int assetCount = CountAssetsInFolder(ScriptableObjectspath);
        //Debug.Log("Number of assets in folder: " + assetCount);
        
        invalidLevels = new List<InvalidLevelSafe>();
        ScriptableInvalidLevel[] loadedObjects = Resources.LoadAll<ScriptableInvalidLevel>("D:/Repos/GameProgramming/ExoPrototype2/Assets/Prefabs");
        Debug.Log(loadedObjects.Length);
        foreach (var guid in loadedObjects)
        {
             InvalidLevelSafe invalidLevelSafe = new InvalidLevelSafe(guid.chunkSizeI, guid.chunkSizeZI, guid.offsetI);
             invalidLevels.Add(invalidLevelSafe);
        }
   }

   // Count the number of Scriptable Objects in the folder LEGACY: Only worked in Unity Editor, not in Build
   // Each invalid level used to be own object
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

   // Create a new Scriptable Object for the Invalid Level
   public void CreateInvalidLevelObj()
   {
        //Create new Scriptable Object of ScriptableInvalidLevel
        ScriptableInvalidLevel invalidLevel = ScriptableObject.CreateInstance<ScriptableInvalidLevel>();
        invalidLevel.chunkSizeI = GetComponent<PerlinNoiseGen>().chunkSize;
        invalidLevel.chunkSizeZI = GetComponent<PerlinNoiseGen>().chunkSizeZ;
        invalidLevel.offsetI = GetComponent<PerlinNoiseGen>().offset;
        //safe Scriptable Object to Assets Folder

        string path = "Assets/Prefabs/Levels/InvalidLevels/level.asset"; // Set your desired path
        AssetDatabase.CreateAsset(invalidLevel, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        invalidLevels.Add(new InvalidLevelSafe(GetComponent<PerlinNoiseGen>().chunkSize, GetComponent<PerlinNoiseGen>().chunkSizeZ, GetComponent<PerlinNoiseGen>().offset));
   }
   
   //Check if Data equals an Invalid Level, called in PerlinNoiseGen
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

    /*private void Update()
    {
         if (clearInvalidLevels)
         {
              invalidLevels.Clear();
         }

         if (createInvalidLevels)
         {
              CreateInvalidLevelObj();
         }
    }*/
#endif
}
