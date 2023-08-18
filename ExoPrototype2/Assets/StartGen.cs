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
    public bool genPathOnly = false;
    public bool startGen = false;
    private PerlinNoiseGen _perlinNoiseGen;

    private void Awake()
    {

        if (genPathOnly) WorldManager.Instance.InitializeGrid();

        _perlinNoiseGen = GetComponent<PerlinNoiseGen>();
        if (startGen)
        {
            if (raceMode)
            {
                // GameModeManager.Instance.InitRace();
                _perlinNoiseGen.Generate();
                if (PerlinNoiseGen.Instance.isDone) WorldManager.Instance.InitializeGrid();
                GameModeManager.Instance.InitRace();
            }
            else
            {
                //  GameModeManager.Instance.InitShooter();
                _perlinNoiseGen.Generate();
                if (PerlinNoiseGen.Instance.isDone) WorldManager.Instance.InitializeGrid();
                GameModeManager.Instance.InitShooter();
            }
        }
    }

}
