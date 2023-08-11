using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeesPlayer : MonoBehaviour
{
    public delegate void SeesPlayer(GameObject enemy);
    public delegate void FindPlayer(GameObject enemy, GameObject player);
    public static event SeesPlayer CanSee;
    public static event FindPlayer GoFindPlayer;
    public static event SeesPlayer CantSee;


    //If Player is in a specific range of the player, then the event is triggered
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            Debug.Log("Enemy sees Player");
            CanSee(this.gameObject);
            GoFindPlayer(this.gameObject, other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy does not see Player");
            CantSee(this.gameObject);
        }
    }
}
