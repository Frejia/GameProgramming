using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] WorldManager world;
    [SerializeField] PerlinNoiseGen noiseGen;
    [SerializeField] public List<GameObject> waypoints;
    [SerializeField] public List<Vector3> curvePoints;
    // Start is called before the first frame update
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        Debug.Log("Creating World");

        int i = 0;
       foreach (var point in waypoints)
       {
           curvePoints.Add(point.transform.position);
       }
       
       noiseGen.Generate();
       world.InitializeGrid();

    }
    
    

}
