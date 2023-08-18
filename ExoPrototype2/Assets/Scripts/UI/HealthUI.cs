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
        Health.Player1GotHit += ApplyDamagePlayer1;
        Health.Player2GotHit += ApplyDamagePlayer2;
    }
    
    private void OnDisable()
    {
        Health.Player1GotHit -= ApplyDamagePlayer1;
        Health.Player2GotHit -= ApplyDamagePlayer2;
    }
    
    public int damageThreshold = 10;

    private int currentDamagePlayer1 = 0;
    private int currentDamagePlayer2 = 0;

    // Call this method to apply damage to the player
    public void ApplyDamagePlayer1(int damage)
    {
        currentDamagePlayer1 += damage;

        while (currentDamagePlayer1 >= damageThreshold)
        {
            if (healthBarsPlayer1.Count > 0)
            {
                GameObject healthBar = healthBarsPlayer1[0];
                healthBarsPlayer1.RemoveAt(0);
                healthBar.SetActive(false);

                currentDamagePlayer1 -= damageThreshold;
            }
            else
            {
                // No more health bars to disable
                break;
            }
        }
    }
    
    public void ApplyDamagePlayer2(int damage)
    {
        currentDamagePlayer2 += damage;

        while (currentDamagePlayer2 >= damageThreshold)
        {
            if (healthBarsPlayer2.Count > 0)
            {
                GameObject healthBar = healthBarsPlayer2[0];
                healthBarsPlayer2.RemoveAt(0);
                healthBar.SetActive(false);

                currentDamagePlayer2 -= damageThreshold;
            }
            else
            {
                // No more health bars to disable
                break;
            }
        }
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
