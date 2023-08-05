using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform firePointFront;
    [SerializeField] private Transform firePointBelow;
    public bool isFiring = false;
    private bool playerClose = true;

    [SerializeField] public PatternManager patternManager;
    [SerializeField] private float fireRate = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        patternManager = this.gameObject.GetComponent<PatternManager>();
    }
    private void Awake()
    {
       /* controls = new PlayerControls();
        controls.Enable();

        // Register the shooting method to the "Fire" action
        controls.Player.Fire.performed += ctx => Shoot();*/
    }

     void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("We Shoot");
            if (!isFiring)
            {
                StartCoroutine(StartPattern());
            }
        }
    }

     /*private IEnumerator ReadBulletPatterns()
     {
         if (Cooldown > 0f)
         {
             // Set the isOnCooldown flag to true and start the cooldown timer
             isOnCooldown = true;
             isFiring = false;
             yield return new WaitForSeconds(Cooldown);
             isOnCooldown = false;
         }
     }
     */
     
     
     private IEnumerator StartPattern()
     {
         isFiring = true;
         // Cone Pattern Example
         //patternManager.SetBulletPattern(firePointFront.gameObject, BulletPatternEnum.BulletPatternsEnum.Cone, BulletBehaviour.BulletBehaviours.None, 40,90, 0.6f, false, 10, 10f);
         // Straight Pattern Example
         patternManager.SetBulletPattern(firePointFront.gameObject, BulletPatternEnum.BulletPatternsEnum.Straight, BulletBehaviour.BulletBehaviours.None, 1,1, 0.2f, true, 1, 10f);
         // Circle Pattern Example
         //patternManager.SetBulletPattern(firePointBelow.gameObject, BulletPatternEnum.BulletPatternsEnum.Circle, BulletBehaviour.BulletBehaviours.None, 0,360, 0.2f, false, 20, 10f);
         
         yield return new WaitForSeconds(0.1f);
         patternManager.SetBulletPatternNone();
         isFiring = false;
     }
     
    private void Shoot()
    {
        patternManager.SetBulletPattern(firePointFront.gameObject, BulletPatternEnum.BulletPatternsEnum.Cone, BulletBehaviour.BulletBehaviours.None, 40,90, 0.6f, 
            false, 10, 10f);

        /*Debug.Log("We shoot!");
        GameObject bul = BulletPool.Instance.GetBulletPlayer();
        bul.transform.position = firePoint.position;
        bul.transform.rotation = firePoint.rotation;
        bul.SetActive(true);
        bul.GetComponent<Bullet>().SetDirection(new Vector3(1,0,1));
        bul.GetComponent<Bullet>().SetSpeed(30f);*/
        
        
    }
    
}
