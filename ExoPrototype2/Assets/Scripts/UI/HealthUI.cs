using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> healthBars;
    private int health;
    private GameObject playerHealthBar;
    private GameObject player2HealthBar;

    private void OnEnable()
    {
        Health.Player1GotHit += UpdateHealth;
        Health.Player2GotHit += UpdateHealth;
    }
    
    private void OnDisable()
    {
        Health.Player1GotHit -= UpdateHealth;
        Health.Player2GotHit -= UpdateHealth;
    }

    private void UpdateHealth(GameObject player, GameObject attacker)
    {
       // Every time the player loses 10 health points or more, the health bar will be updated
       if(player.GetComponent<Health>().currentHealth % 10 == 0)
       {
           health = (int) player.GetComponent<Health>().currentHealth / 10;
           if (player.CompareTag("Player"))
           {
               playerHealthBar = healthBars[health];
               playerHealthBar.SetActive(false);
           }
           else
           {
               player2HealthBar = healthBars[health];
               player2HealthBar.SetActive(false);
           }
       }
    }
}
