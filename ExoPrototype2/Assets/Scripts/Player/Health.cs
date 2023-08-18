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
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public float currentHealth;
    [SerializeField] public bool isImmune;
    [SerializeField] public float immuneDuration = 3f;
    [SerializeField] public bool isDead = false;
    [SerializeField] private GameObject attacker;
    
    public delegate void Hit(GameObject enemy, GameObject attacker);

    public delegate void HitSound(int index);
    public static event Hit EnemyKilledBy;
    public static event Hit PlayerKilledBy;
    public static event Hit Player1GotHit;
    public static event Hit Player2GotHit;
    public static event HitSound PlayerHitSound;
    public static event HitSound PlayerDead;
    public static event HitSound EnemyHitSound;
    public static event HitSound EnemyDead;
    
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void GetsHit(int damage, GameObject attacker){
       
        this.attacker = attacker;
    if (!isImmune) // Check if not already immune
    {
        currentHealth -= damage;

        // Time of immunity after getting hit
        StartCoroutine(Immunity());

        if (currentHealth > 0)
        {
            if (this.gameObject.CompareTag("Enemy"))
            {
                EnemyHitSound(8);
            }
            else
            {
                PlayerHitSound(8);
            }
        }
        
        if(this.gameObject.CompareTag("Player"))
        {
            Player1GotHit(this.gameObject, attacker);
        }
        if(this.gameObject.CompareTag("Player2"))
        {
            Player2GotHit(this.gameObject, attacker);
        }

        if (currentHealth < 0) currentHealth = -1;
    }
    CheckDeath();
}

private IEnumerator Immunity()
{
    isImmune = true; // Set immunity flag
    // Add any visual or audio feedback for immunity

    yield return new WaitForSeconds(immuneDuration); // Duration of immunity

    isImmune = false; // Reset immunity flag
}

    private void CheckDeath()
    {
        if(currentHealth <= 0)
        {
            isDead = true;
            //Dissolve Shield TO DO: Compare layer instead of tag
            if (this.gameObject.transform.parent.gameObject.CompareTag("Enemy"))
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
}
