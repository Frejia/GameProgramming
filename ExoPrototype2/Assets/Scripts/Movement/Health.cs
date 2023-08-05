using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void GetsHit(int damage)
    {
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
            this.GetComponent<Dissolve>().isdissolving = true;
            //Destroy Object
            Destroy(this.transform.parent, 5f);
        }
    }
}
