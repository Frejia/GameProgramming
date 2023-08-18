using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> healthBarsPlayer1;
    [SerializeField] private List<GameObject> healthBarsPlayer2;
    private int health;
    
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
               // Deactivate the amount of health bars that the player has lost
                for (int i = 0; i < healthBarsPlayer1.Count; i++)
                {
                     if (i >= health)
                     {
                          healthBarsPlayer1[i].SetActive(false);
                     }
                }
           }
           else
           {
               for (int i = 0; i < healthBarsPlayer2.Count; i++)
               {
                   if (i >= health)
                   {
                       healthBarsPlayer2[i].SetActive(false);
                   }
               }
           }
       }
    }
}
