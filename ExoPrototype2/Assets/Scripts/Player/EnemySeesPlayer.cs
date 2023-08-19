using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Enemy seeing the player and all related Event delegates
///
/// Used for EnemyPatternManager and Enemy Movement
/// Applied to every Enemy in the game
/// </summary>
public class EnemySeesPlayer : MonoBehaviour
{
   
    // ------ EVENT DELEGATES ------
    public delegate void SeesPlayer(GameObject enemy);
    public delegate void FindPlayer(GameObject enemy, GameObject player);
    public static event SeesPlayer CanSee;
    public static event FindPlayer GoFindPlayer;
    public static event SeesPlayer CantSee;


    //If Player is in a specific range of the player, then the event is triggered
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something close to Enemy");
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            this.GetComponent<EnemyPatternManager>().PlayerClose(this.gameObject);
            GoFindPlayer(gameObject, other.gameObject);
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        { Debug.Log("Player out of range");
            this.GetComponent<EnemyPatternManager>().StopPatterns(this.gameObject);
        }
    }
}
