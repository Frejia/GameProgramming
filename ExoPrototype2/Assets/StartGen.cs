using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGen : MonoBehaviour
{
    private PerlinNoiseGen _perlinNoiseGen;
    
    private void Awake()
    {
        _perlinNoiseGen = GetComponent<PerlinNoiseGen>();
        _perlinNoiseGen.Generate();
        if(PerlinNoiseGen.Instance.isDone)WorldManager.Instance.InitializeGrid();
    }
    
}
