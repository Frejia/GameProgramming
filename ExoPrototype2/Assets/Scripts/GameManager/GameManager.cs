using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] WorldManager world;
    [SerializeField] PerlinNoiseGen noiseGen;
    // Start is called before the first frame update
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }

        StartCoroutine(noiseGen.Generate());
      if(noiseGen.isDone)world.InitializeGrid();
    }
    
    

}
