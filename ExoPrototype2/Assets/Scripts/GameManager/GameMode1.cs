using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode1 : MonoBehaviour
{
    //Player Points
    public int points1;
    public int points2;
    
    //true if win, false if lose
    public bool win;

    [SerializeField] public Transform goal, start;
    [SerializeField] public List<GameObject> waypoints;
    
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
    
    
    // -------- RACE MODE ------------
    private void InitRace()
    {
        // Get Start and End Point, place a goal/Start there
        
        //Get random points on Curve 
    }
    
}
