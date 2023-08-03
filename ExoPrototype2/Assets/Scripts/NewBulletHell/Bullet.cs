using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] public float speed = 30f;
    private float savedSpeed;
    [SerializeField] private float _bulletLifeTime = 5f;

    [SerializeField] private ParticleSystem impactEffect;
   // [SerializeField] private SpriteRenderer renderer;
    
    [SerializeField, Range(10, 20)] private int _damage = 10;

    [SerializeField] public bool _isEnemyBullet = false;

    public int GetDamage()
    {
        return _damage;
    }

    private void Awake()
    {
        savedSpeed = speed;
    }

    private void OnEnable()
    {
       // renderer.enabled = true;
        speed = savedSpeed;
        Invoke("Destroy", _bulletLifeTime);
        impactEffect.Stop();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }

    /*private void OnCollisionEnter(Collision other)
    {
        impactEffect.Play();
        StartCoroutine(WaitForParticleSystem());
    } */

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Health>().GetsHit(_damage);
        impactEffect.Play();
        StartCoroutine(WaitForParticleSystem());
        speed = 0f;
        /*if (other.gameObject.layer == 7)
            {
                impactEffect.Play();
                StartCoroutine(WaitForParticleSystem());
                speed = 0f;
            }
        
        if (other.gameObject.layer == 8)
        {
            impactEffect.Play();
            StartCoroutine(WaitForParticleSystem());
            speed = 0f;
        } else if (other.gameObject.layer == 9)
        {
            impactEffect.Play();
            StartCoroutine(WaitForParticleSystem());
            speed = 0f;
        } else if (other.gameObject.layer == 13)
        {
            impactEffect.Play();
            StartCoroutine(WaitForParticleSystem());
            speed = 0f;
        }*/


    }

    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    private IEnumerator WaitForParticleSystem()
    {
        //renderer.enabled = false;
        yield return new WaitForSeconds(impactEffect.main.duration);
        Destroy();
    }
}
