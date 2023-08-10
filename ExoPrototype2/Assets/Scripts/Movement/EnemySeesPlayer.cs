using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeesPlayer : MonoBehaviour
{
    public delegate void SeesPlayer(GameObject enemy);
    public static event SeesPlayer CanSee;
    public static event SeesPlayer CantSee;


    //If Player is in a specific range of the player, then the event is triggered
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy sees Player");
            CanSee(this.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy does not see Player");
            //CantSee(this.gameObject);
        }
    }
}
