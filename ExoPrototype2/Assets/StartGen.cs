using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGen : MonoBehaviour
{
    public bool raceMode = false;
    private PerlinNoiseGen _perlinNoiseGen;
    
    private void Awake()
    {
        _perlinNoiseGen = GetComponent<PerlinNoiseGen>();
        if (raceMode)
        {
          // GameModeManager.Instance.InitRace();
           _perlinNoiseGen.Generate();
           if (PerlinNoiseGen.Instance.isDone) WorldManager.Instance.InitializeGrid();
        }
        else
        {
          //  GameModeManager.Instance.InitShooter();
            _perlinNoiseGen.Generate();
            if (PerlinNoiseGen.Instance.isDone) WorldManager.Instance.InitializeGrid();
        }
    }
    
}
