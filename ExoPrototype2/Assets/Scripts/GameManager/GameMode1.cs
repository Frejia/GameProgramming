using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameMode1 : MonoBehaviour
{
    //Player Points
    public int points1;
    public int points2;
    
    //true if win, false if lose
    public bool win;

    private PerlinNoiseGen perlin;
    [SerializeField] private Transform goal, start;
    [SerializeField] private GameObject portal;
    
    //--- Game Modes
    /*
     Player vs AI
     Player can fly through a level and fight enemies like in a bullet hell shooter
     
     Player vs Player
     Players can fight one another in bullet hell style
     
     Player vs Player Hide and Seek
     In a more elaborate map, players can hide and seek one another
     
     Player vs AI Hide and Seek
     Players can hide from AI
     
     Player vs Player race
     There is a start and goal and the players have to race one another there
     
     */
    private void Start()
    {
        perlin = GetComponent<PerlinNoiseGen>();
        InitRace();
    }

    // -------- RACE MODE ------------
    private void InitRace()
    {
        // Get Start and End Point, place a goal/Start there
        start = perlin.waypoints[0].transform;
        goal = perlin.waypoints[perlin.waypoints.Count-1].transform;
        //Get random points from the ones that are left
        int randomPoint = Random.Range(1, perlin.waypoints.Count - 2);
        //Get Direction to previous point
        Vector3 dir = (perlin.waypoints[randomPoint].transform.position - perlin.waypoints[randomPoint - 1].transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            
        Instantiate(portal, perlin.waypoints[randomPoint].transform.position * 5, rotation);
    }
    
}
