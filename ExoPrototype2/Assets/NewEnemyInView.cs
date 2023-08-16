using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyInView : Aim
{
    public delegate void EnemyInScreen(GameObject enemy);
    public static event EnemyInScreen OnEnemyInScreen;
    public static event EnemyInScreen OnEnemyNotInScreen;

    [SerializeField] private List<GameObject> targets;
    [SerializeField] private GameObject target;
    
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
            }
            else
            {
                target = other.gameObject;
            }
            Debug.Log(gameObject.name + " is visible");
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
