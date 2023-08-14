using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachGoal : MonoBehaviour
{
    private int checkPoints;
    public delegate void RaceEvent(GameObject player);
    
    public static event RaceEvent ReachedGoal;
    
    // Check if player reached goal first 
    private bool reachedGoal = false;

    private void Start()
    {
        checkPoints = PerlinNoiseGen.Instance.waypoints.Count - 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!reachedGoal)
        {
            ReachedGoal(other.gameObject);
            reachedGoal = true;
        }
        else
        {
            // A Player already won
        }
    }
}
