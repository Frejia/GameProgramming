using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Handles all movement and stats of a bullet
///
/// used in Bullet Pool
/// </summary>
public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] public float speed = 30f;
    [SerializeField] private float _bulletLifeTime = 4f;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField, Range(10, 20)] private int _damage = 10;
   
    // Bullet Type Variables
    public bool isEnemyBullet = false;
    public bool friendlyFire = false;
    
    // Other variables
    private Vector3 direction;
    private float savedSpeed;
    private GameObject attacker;

    private void Awake()
    {
        savedSpeed = speed;
    }
    
    private void OnEnable()
    {
        speed = savedSpeed;
        Invoke("Destroy", _bulletLifeTime);
        impactEffect.Stop();
    }
    
    // Who is using the Bullet? --> Ensures that bullet has correct collision and points are counted
    public void SetUser(GameObject user)
    {
        attacker = user;
    }

    // ----- Life Cycle -----
    // Moves the bullet when it is enabled
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    private void Destroy()
    {
        attacker = null;
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        attacker = null;
        CancelInvoke();
    }
    
    // ----- Collision -----
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        // If it is a player bullet, it can also hit an enemy
        if (!isEnemyBullet)
        {
            if (other.gameObject.layer == 7)
            {
                if (other.GetComponent<Health>() != null)
                {
                    other.GetComponent<Health>().GetsHit(_damage, attacker);
                }

                impactEffect.Play();
                StartCoroutine(WaitForParticleSystem());
                speed = 0f;
            }
        }

        // Handle Player vs Player and Enemy vs Player Collision
        if (other.gameObject.layer == 13)
                    {
                        if (friendlyFire && attacker.gameObject.tag == "Player")
                        {
                            if (other.GetComponent<Health>() != null)
                            {
                                other.GetComponent<Health>().GetsHit(_damage, attacker);
                            }
            
                            impactEffect.Play();
                            StartCoroutine(WaitForParticleSystem());
                            speed = 0f;
                        }
                        
                        if (attacker.gameObject.tag == "Enemy")
                        {
                            if (other.GetComponent<Health>() != null)
                            {
                                other.GetComponent<Health>().GetsHit(_damage, attacker);
                            }
            
                            impactEffect.Play();
                            StartCoroutine(WaitForParticleSystem());
                            speed = 0f;
                        }
                    }
                    
                    if (other.gameObject.layer == 8)
                    {
                        if (friendlyFire && attacker.gameObject.tag == "Player2")
                        {
                            if (other.GetComponent<Health>() != null)
                            {
                                other.GetComponent<Health>().GetsHit(_damage, attacker);
                            }
            
                            impactEffect.Play();
                            StartCoroutine(WaitForParticleSystem());
                            speed = 0f;
                        }
                        
                        if (attacker.gameObject.tag == "Enemy")
                        {
                            if (other.GetComponent<Health>() != null)
                            {
                                other.GetComponent<Health>().GetsHit(_damage, attacker);
                            }
            
                            impactEffect.Play();
                            StartCoroutine(WaitForParticleSystem());
                            speed = 0f;
                        }
                    }
    }

  
// Play particle System
    private IEnumerator WaitForParticleSystem()
    {
        //renderer.enabled = false;
        yield return new WaitForSeconds(impactEffect.main.duration);
        Destroy();
    }
}
