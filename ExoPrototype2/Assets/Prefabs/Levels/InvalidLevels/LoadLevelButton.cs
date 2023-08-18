using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadLevelButton : MonoBehaviour
{
    private PerlinNoiseGen perlin;
    [SerializeField] private ScriptableInvalidLevel savedLevels;
    [SerializeField] public int levelIndex;
    
    public void LoadLevel()
    {
        perlin = PerlinNoiseGen.Instance;
        
        string filePath = Path.Combine(Application.persistentDataPath, "SavedLevel.asset");
        if (File.Exists(filePath))
        {
            // Access Data
            string json = File.ReadAllText(filePath);
            savedLevels = JsonUtility.FromJson<ScriptableInvalidLevel>(json);

            // Set the values of the PerlinNoiseGen
            perlin.PerlinSetter(savedLevels.chunkSize[levelIndex],savedLevels.chunkSizeZ[levelIndex], savedLevels.offset[levelIndex], savedLevels.raceMode[levelIndex],
                savedLevels.withCurve[levelIndex], savedLevels.sphere[levelIndex], savedLevels.meshSmoothing[levelIndex], savedLevels.wayPoints[levelIndex]);
           
        }
        else
        {
            Debug.LogWarning("Saved data file not found.");
        }
    }

    private void StartScene()
    {
        
    }
}
