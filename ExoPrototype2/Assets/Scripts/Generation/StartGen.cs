using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Handles the cross-scene generation of the world from Main Menu to actual game scene
/// Exists only within the scenes that require the Terrain or Paths to be generated
/// </summary>
public class StartGen : MonoBehaviour
{
    public bool raceMode = false;
    public bool genPathOnly = false;
    public bool startGen = false;
    private PerlinNoiseGen _perlinNoiseGen;

    
    private void Awake()
    {
        // Within pre-generated scenes, only the Pathfinding needs to be generated
        if (genPathOnly) WorldManager.Instance.InitializeGrid();

        _perlinNoiseGen = GetComponent<PerlinNoiseGen>();
        // start the generation if the scene is not pre-generated
        if (startGen)
        {
            // Init the Racer Mode
            if (raceMode)
            {
                // GameModeManager.Instance.InitRace();
                _perlinNoiseGen.Generate();
                GameModeManager.Instance.InitRace();
            }
            else
            {
                // Init Shooter Mode
                _perlinNoiseGen.Generate();
                if (PerlinNoiseGen.Instance.isDone) WorldManager.Instance.InitializeGrid();
                GameModeManager.Instance.InitShooter();
            }
        }
    }

}
