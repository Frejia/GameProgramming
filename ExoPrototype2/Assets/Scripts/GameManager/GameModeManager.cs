using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

/// <summary>
/// Handles the different Game Mode Instantiations and the Win Conditions
/// </summary>
public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance;
    
    // ------- SHOOTER MODE ---------
    public int points1;
    public int points2;
    //true if win, false if lose
    public bool win;
    
    [Header("InGameUI References")]
    [SerializeField] private TextMeshProUGUI points1Text;
    [SerializeField] private TextMeshProUGUI points2Text;

    [Header("WorldGen References")]
    [SerializeField] WorldManager world;
    [SerializeField] PerlinNoiseGen noiseGen;
    public bool debug;
    
    // Events
    public delegate void Win();
    public static event Win Player1Win;
    public static event Win Player2Win;

    // ------- RACER MODE ---------
    [SerializeField] private Transform goal, start;
    [SerializeField] private List<Transform> checkPoints;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject goalParticles;

    /// <summary>
    /// ------- GAME MODES ---------
    ///Player vs AI Shooter
    ///Player can fly through a level and fight enemies like in a bullet hell shooter
    ///
    ///Player vs Player Shooter
    /// Players can fight one another in bullet hell style
    ///
    ///Player vs Player Racer
    /// There is a start and goal and the players have to race one another there, Enemies try to stop them
    /// </summary>
    ///
    private void Awake()
    {
        Instance = this;
        world = WorldManager.Instance;
        noiseGen = PerlinNoiseGen.Instance;
    }

    private void Start()
    {
        // Get All References when going to new Scene from Main Menu Scene
        /*points1Text = GameObject.Find("Points1").GetComponent<TextMeshProUGUI>();
        points2Text = GameObject.Find("Points2").GetComponent<TextMeshProUGUI>();*/
        
        // Racer Mode References
        start = noiseGen.waypoints[0].transform;
        goal = noiseGen.waypoints[noiseGen.waypoints.Count - 1].transform;
        
        // Shooter Mode Point Handling
        Health.EnemyGotHit += CountPoints;
        ReachGoal.ReachedGoal += EndRace;
        
        // Race Mode Point Handling
        PlayerInputManager.instance.onPlayerJoined += PlacePlayer;

        /*points1 = 0;
        points2 = 0;
        points1Text.text = points1.ToString();
        points2Text.text = points2.ToString();*/
    }
    
    // Count poitns when Enemy is elemenated and show in UI
    private void CountPoints(GameObject enemy, GameObject player)
    {
        int points = 0;
        
        if (enemy.name.Equals("BigShip"))
        {
            points = 4;
        }
        else
        {
            points = 2;
        }

        if (player.tag == "Player")
        {
            points1 += points;
        }
        else
        {
            points2 += points;
        }
        
        if(points1 >= 10 || points2 >= 10)
        {
            WinCheck();
        }
        
        points1Text.text = points1.ToString();
        points2Text.text = points2.ToString();
    }

    // Check which player has won and send Win Event to change GameState
    private void WinCheck()
    {
        if (points1 > points2)
        {
            win = true;
            Debug.Log("Player 1 Won");
            Player1Win();
        }
        else
        {
            win = true;
            Debug.Log("Player 2 Won");
            Player2Win();
        }
    }

    // -------- SHOOTER INIT ------------
    public void InitShooter()
    {
        Generate();
    }
    
    // Generate Terrain
    private void Generate()
    {
             for (int i = 0; i < noiseGen.waypoints.Count - 1; i++){
                 //Get Direction to previous point
                Vector3 dir = (noiseGen.waypoints[i].transform.position - noiseGen.waypoints[i+1].transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
                
                Instantiate(portal, noiseGen.waypoints[i].transform.position * 5, rotation);
            }
            Instantiate(portal, noiseGen.waypoints[noiseGen.waypoints.Count-1].transform.position * 5, Quaternion.identity);
            
    
        noiseGen.raceMode = true;
        noiseGen.withCurve = false;
        
        if (!debug)
        {
            StartCoroutine(noiseGen.Generate());
            if(noiseGen.isDone)world.InitializeGrid();
        }
        
        
    }
    
    // -------- RACE MODE INIT ------------
    public void InitRace()
    {
        Debug.Log("init Racer!");
        PlayerInputManager.instance.EnableJoining();
        PlayerInputManager.instance.JoinPlayer();
        GenerateRace();

        for(int i = 1; i < noiseGen.waypoints.Count - 2; i++)
        {
            // Do not add Start and Finish to Checkpoints
            checkPoints.Add(noiseGen.waypoints[i].transform);
        }
        
        // Get Start and End Point, place a goal/Start there
        start = noiseGen.waypoints[0].transform;
        goal = noiseGen.waypoints[noiseGen.waypoints.Count-1].transform;
        //Get random points from the ones that are left
        int randomPoint = Random.Range(1, noiseGen.waypoints.Count - 2);
        //Get Direction to previous point
        Vector3 dir = (noiseGen.waypoints[randomPoint].transform.position - noiseGen.waypoints[randomPoint - 1].transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            
        Instantiate(portal, noiseGen.waypoints[randomPoint].transform.position * 5, rotation);
        
        // Spawn players at Start of Race
        GameManager.Instance.player1.transform.position = start.position;
    }
    
    // Generate Terrain
    private void GenerateRace()
    {
        noiseGen.raceMode = true;
        noiseGen.withCurve = true;
        StartCoroutine(noiseGen.Generate());
        //if(noiseGen.isDone)world.InitializeGrid();
    }

    private void PlacePlayer(PlayerInput player)
    {
        player.gameObject.transform.position = new Vector3(start.position.x + 10, start.position.y + 10, start.position.z + 10);
    }

    private void StartRace()
    {
       
    }

    // Check which player has won and send Win Event to change GameState
    // Connected to the ReachGoal Event on the Goal Object
    private void EndRace(GameObject player)
    {
        if(player.tag == "Player")
        {
            win = true;
            Debug.Log("Player 1 Won");
            Player1Win();
        }
        else
        {
            win = true;
            Debug.Log("Player 2 Won");
            Player2Win();
        }
    }
    
}
