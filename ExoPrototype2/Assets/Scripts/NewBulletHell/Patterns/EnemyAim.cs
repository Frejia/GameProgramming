using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : Aim
{
    private void OnEnable()
    {
        EnemySeesPlayer.GoFindPlayer += UpdateTarget;
    }
    
    private void OnDisable()
    {
        EnemySeesPlayer.GoFindPlayer -= UpdateTarget;
    }

    private void UpdateTarget(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<EnemyAim>().target = player;
        enemy.GetComponent<EnemyAim>().user = enemy;
    }
}
