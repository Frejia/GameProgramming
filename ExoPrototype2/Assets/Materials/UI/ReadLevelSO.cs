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

    private void LoadLevelList(int i)
    {
        GameObject button = Instantiate(ButtonPrefab, transform);
        button.transform.parent = ScrollList.transform;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + i;
        button.GetComponent<LoadLevelButton>().levelIndex = i;
    }
    
    
}    

