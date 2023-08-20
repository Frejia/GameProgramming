using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Handles Health Stats of Enemies and Players
///
/// When the Object gets hit, it will take damage and check if it is dead
/// Sends to GameManager whether player is dead or an enemy is dead to count points
/// </summary>
public class Health : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public float currentHealth;
    [SerializeField] public bool isImmune;
    [SerializeField] public float immuneDuration = 3f;
    [SerializeField] public bool isDead = false;
    [SerializeField] private GameObject attacker;
    
    // Hit Events
    public delegate void Hit(GameObject enemy, GameObject attacker); // For GameManager
    public delegate void HitDamage(int damage); // To Update Healthbar

    public delegate void HitSound(int index); // For Playing Sounds in the SoundLibrary
    public static event Hit EnemyKilledBy;
    public static event Hit PlayerKilledBy;
    public static event HitDamage Player1GotHit;
    public static event HitDamage Player2GotHit;
    public static event HitSound PlayerHitSound;
    public static event HitSound PlayerDead;
    public static event HitSound EnemyHitSound;
    public static event HitSound EnemyDead;
    
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Gets activated when a bullet encounters an object that has health
    /// Handles all hit operations
    /// </summary>
    public void GetsHit(int damage, GameObject attacker){
       
        this.attacker = attacker;
    if (!isImmune) // Check if not already immune
    {
        currentHealth -= damage;

        // Time of immunity after getting hit
        StartCoroutine(Immunity());

        // Add any audio feedback for getting hit
        if (currentHealth > 0)
        {
            if (this.gameObject.CompareTag("Enemy"))
            {
                EnemyHitSound(8);
            }
            else
            {
                PlayerHitSound(8);
                if(this.gameObject.CompareTag("Player"))
                {
                    Player1GotHit(damage);
                }
                if(this.gameObject.CompareTag("Player2"))
                {
                    Player2GotHit(damage);
                }
            }
        }
        
        // Make sure object cannot go infinitely below 0 health
        if (currentHealth < 0) currentHealth = -1;
    }
    CheckDeath();
}

    /// <summary>
    ///  Set immunity flag and wait for duration
    /// Immunity is required to not overload player with damage when he is hit by multiple enemies
    /// </summary>
    private IEnumerator Immunity()
{
    isImmune = true; // Set immunity flag
    // Add any visual or audio feedback for immunity

    yield return new WaitForSeconds(immuneDuration); // Duration of immunity

    isImmune = false; // Reset immunity flag
}

/// <summary>
/// Check if Player dies or Enemy dies and send event in that case
/// </summary>
    private void CheckDeath()
    {
        if(currentHealth <= 0)
        {
            isDead = true;
            //Dissolve Shield TO DO: Compare layer instead of tag
            if (gameObject.layer == 7)
            {
                this.GetComponent<Dissolve>().isdissolving = true;
                Debug.Log("Enemy is killed");
                EnemyKilledBy(this.transform.parent.gameObject, attacker);
                EnemyDead(7);
                //Destroy Object
                this.transform.parent.gameObject.SetActive(false);
            }
            
            if (this.gameObject.tag == "Player")
            {
                PlayerKilledBy(this.gameObject, attacker);
                PlayerDead(4);
                // GameManager.Instance.SetLose();
            }
            if (this.gameObject.tag == "Player2")
            {
                PlayerKilledBy(this.gameObject, attacker);
                PlayerDead(4);
                GameManager.Instance.SetLose();
            }
           
        }
    }

/// <summary>
/// When player respawns, he needs his full health back
/// </summary>
public void ResetHealth(GameObject player)
    {
        currentHealth = maxHealth;
        isDead = false;
    }
}
