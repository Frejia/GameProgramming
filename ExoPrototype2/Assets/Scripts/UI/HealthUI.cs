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
    public int damage;

    // Call this method to apply damage to the player
    public void ApplyDamagePlayer1(int damage)
    {
        this.damage = damage;
        currentDamagePlayer1 = damage;

        if (currentDamagePlayer1 >= damageThreshold)
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
            }
        }
    }
    
    public void ApplyDamagePlayer2(int damage)
    {
        currentDamagePlayer2 += damage;

        if (currentDamagePlayer2 >= damageThreshold)
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
            }
        }
    }
    
}
