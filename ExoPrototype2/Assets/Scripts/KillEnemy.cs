using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Health.EnemyKilledBy += Kill;
    }

    private void Kill(GameObject enemy, GameObject player)
    {
       Destroy(enemy);
    }
}
