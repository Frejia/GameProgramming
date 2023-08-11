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
    [Header("Shoot Control Settings")] 
    [SerializeField] private float cooldown1, cooldown2;
    public bool shooting;
    public bool shotSpecial;
    public bool shotSpecial2;

    
    [SerializeField] private Transform firePointFront;
    [SerializeField] private Transform firePointBelow;
    public bool isFiring = false;
    private bool playerClose = true;

    private PatternManager patternManager;
    public delegate void Shoot(int i);
    public static event Shoot Shot;

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
         HandleShooting();
     }

     private void HandleShooting()
     {
         if (shooting)
         {
             StartCoroutine(StartPattern(0));
         }

         if (shotSpecial)
         {
             StartCoroutine(StartPattern(1));
         }

         if (shotSpecial2)
         {
             StartCoroutine(StartPattern(2));
         }
         
     }


     private IEnumerator StartPattern(int shot)
     {
         isFiring = true;
         if (shot == 0)
         {
             Shot(0);
             // Straight Pattern Example
             patternManager.SetBulletPattern(BulletPatternEnum.BulletPatternsEnum.Straight, BulletBehaviour.BulletBehaviours.None, 0,0,5f, false, 1, 10f);

             /*GameObject bul = BulletPool.Instance.GetBulletPlayer();
             bul.transform.position = this.gameObject.transform.position;
             bul.transform.rotation = this.gameObject.transform.rotation;
             bul.SetActive(true);
             bul.GetComponent<Bullet>().SetSpeed(30);
             bul.GetComponent<Bullet>().SetDirection(Vector3.forward);*/
             
         }
         else if(shot == 1)
         {
             // Cone Pattern Example
             Shot(1);
             patternManager.SetBulletPattern(BulletPatternEnum.BulletPatternsEnum.Cone, BulletBehaviour.BulletBehaviours.None, 40,90, 2f, true, 10, 10f);
         }
         else
         {
             // Circle Pattern Example
             Shot(2);
             patternManager.SetBulletPattern(BulletPatternEnum.BulletPatternsEnum.Circle, BulletBehaviour.BulletBehaviours.None, 0,360, 2f, true, 20, 10f);
         }
         
         yield return new WaitForSeconds(0.3f);
         patternManager.SetBulletPatternNone();
         isFiring = false;
     }
     
    public void OnShoot(InputAction.CallbackContext context)
    {
        shooting = context.performed;
    }
    public void OnShootSpecial(InputAction.CallbackContext context)
    {
        shotSpecial = context.performed;
    }
    public void OnShootSpecial2(InputAction.CallbackContext context)
    {
        shotSpecial2 = context.performed;
    }
    
}
