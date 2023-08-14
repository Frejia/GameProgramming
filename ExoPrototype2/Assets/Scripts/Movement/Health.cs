using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private GameObject attacker;
    
    public delegate void Hit(GameObject enemy, GameObject attacker);
    public static event Hit EnemyGotHit;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void GetsHit(int damage, GameObject attacker)
    {
        this.attacker = attacker;
        Debug.Log(gameObject.name + "Took damage");
        currentHealth -= damage;
        Debug.Log(currentHealth);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if(currentHealth <= 0)
        {
            Debug.Log("Object is dead");
            //Dissolve Shield
            if (this.gameObject.tag == "Enemy")
            {
                this.GetComponent<Dissolve>().isdissolving = true;
                EnemyGotHit(this.transform.parent.gameObject, attacker);
                //Destroy Object
                Destroy(this.transform.parent, 5f);
            }
            
            if (this.gameObject.tag == "Player")
            {
               GameManager.Instance.SetLose();
            }
           
        }
    }
}
