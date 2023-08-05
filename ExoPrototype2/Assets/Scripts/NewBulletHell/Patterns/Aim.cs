using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public class Aim : MonoBehaviour
{
    public static Aim Instance { get; private set; }
    public GameObject player;
    public GameObject user { get; set; }
    [SerializeField] private GameObject target;
    [SerializeField] private List<GameObject> targets;
    
    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnEnable()
    {
        ObjectsInView.OnEnemyInScreen += EnemyLockOn;
    }

    private void OnDisable()
    {
        ObjectsInView.OnEnemyInScreen -= EnemyLockOn;
    }

    private GameObject GetNearestEnemy()
    {
        GameObject nearestObject = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy  in targets)
        {
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

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
        if (enemy == null)
        {
            target = null;
            targets.Clear();
        }
        else
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

    }

    public Vector3 Aiming()
    {
        //Calculate Angle for Direction of player from this gameobject position in a 3D Space, including the z axis
        Vector3 direction = player.transform.position - user.transform.position;
        return direction.normalized;
    }

    public Vector3 RandomAim()
    {
        float angle = Random.Range(0, 360);
        
        float bulDirX = user.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = user.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        
        Vector3 bulDir = (bulMoveVector - user.transform.position).normalized;
        return bulDir;
    }
}
