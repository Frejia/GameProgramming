using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : Aim
{
    [SerializeField] private List<GameObject> targets;
    
    void OnEnable()
    {
        ObjectsInView.OnEnemyInScreen += EnemyLockOn;
        ObjectsInView.OnEnemyNotInScreen += RemoveFromTargets;
    }

    private void OnDisable()
    {
        ObjectsInView.OnEnemyInScreen -= EnemyLockOn;
        ObjectsInView.OnEnemyNotInScreen -= RemoveFromTargets;
    }

    private GameObject GetNearestEnemy()
    {
        GameObject nearestObject = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy  in targets)
        {
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, this.gameObject.transform.position);

            // If the calculated distance is less than the current minimum distance, update the nearestObject and minDistance
            if (distanceToPlayer < minDistance)
            {
                nearestObject = enemy;
                minDistance = distanceToPlayer;
            }  
        }
       return nearestObject;
    }

    private void EnemyLockOn(GameObject enemy)
    {
        // If that gameobject is not already in the list, add it
            if (!targets.Contains(enemy))
            {
                targets.Add(enemy);
            }
            // If there is more than one enemy in the list, get the nearest enemy
            if(targets.Count > 1 && enemy != null)
            {
                target = GetNearestEnemy();
            }
            else
            {
                target = enemy;
            }
    }

    private void RemoveFromTargets(GameObject enemy)
    {
        // Remove the enemy from the list if it exists in the list
        if (targets.Contains(enemy))
        {
            targets.Remove(enemy);
        }
        // If there are still enemies in the list, update the nearest enemy
        if (targets.Count > 0)
        {
            target = GetNearestEnemy();
        }
        else
        {
            target = null;
        }
    }
}
