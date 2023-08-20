using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Formerly PlayerAim
///
/// Handles all events of when an enemy is in view of the player
/// Allows Aim to get the closest enemy
/// </summary>
public class NewEnemyInView : Aim
{
    // Events
    public delegate void EnemyInScreen(GameObject enemy);
    public delegate void EnemySeenSound(int index);
    public static event EnemyInScreen OnEnemyInScreen;
    public static event EnemyInScreen OnEnemyNotInScreen;
    public static event EnemySeenSound OnEnemySeenSound;

    [Header("Target Variables")]
    [SerializeField] private List<GameObject> targets; // All currently visible enemies
    [SerializeField] private GameObject target; // Current closest enemy
    
    // Player has a view area that detects enemies
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(!targets.Contains(other.gameObject))
            {
                targets.Add(other.gameObject);
            }

            if(targets.Count > 1)
            {
                target = GetClosestEnemy();
                OnEnemySeenSound(5);
            }
            else
            {
                target = other.gameObject;
                OnEnemySeenSound(5);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (targets.Contains(other.gameObject))
            {
                targets.Remove(other.gameObject);
            }
        }
        
        if (targets.Count > 0)
        {
            target = GetClosestEnemy();
        }
        else
        {
            target = null;
        }
    }
    
    /// <summary>
    /// Gets the currently closest enemy to player as the target
    ///
    /// Can be expanded to be the one the player actually aims at
    /// </summary>
    public GameObject GetClosestEnemy()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 thisPosition = transform.position;

        foreach (GameObject obj in targets)
        {
            float distance = Vector3.Distance(thisPosition, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

       return closestObject;
    }
    
    public GameObject GetClosestTarget()
    {
        return target;
    }
    
}
