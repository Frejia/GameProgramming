using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using Sound;
using TMPro;
using UnityEngine;

public class ReadLevelSO : MonoBehaviour
{
    private PerlinNoiseGen perlin;
    [SerializeField] private ScriptableInvalidLevel savedLevels;
    
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private GameObject ScrollList;
    
    
    public void LoadSavedLevels()
    {
        int buttonCount = savedLevels.chunkSize.Count;
        
        for(int i = 0; i < buttonCount; i++)
        {
            LoadLevelList(i);
        }
    }
    
    public void LoadLevel(int levelIndex)
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

    private void LoadLevelList(int i)
    {
        GameObject button = Instantiate(ButtonPrefab, transform);
        button.transform.parent = ScrollList.transform;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + i;
    }
    
    
}    

