using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using UnityEngine;

public class PatternManager : MonoBehaviour
{

    public BulletPatternEnum.BulletPatternsEnum activebulletPattern;
    private BulletHell.BulletBehaviour.BulletBehaviours activebulletBehaviour;
    public float fireRate;
    public bool isAiming = false;
    public int bulletsAmount;

    private float patternDuration;
    private Vector3 bulletMoveDirection;
    private float cooldown;
    public Vector3 bulDir;
    public float bulletSpeed;
    public float startAngle, endAngle;

    private Aim AimInstance;
    public GameObject user;

    private void Start()
    {
        AimInstance = Aim.Instance;
    }

    public void SetBulletPattern(GameObject user, BulletPatternEnum.BulletPatternsEnum bulletPattern, BulletHell.BulletBehaviour.BulletBehaviours bulletBehaviour, 
        float startAngle, float endAngle, float fireRate, bool isAiming, int bulletsAmount, float bulletSpeed)
    {
        this.user = user;
        AimInstance.user = user;
        this.activebulletPattern = bulletPattern;
        this.activebulletBehaviour = bulletBehaviour;
        this.fireRate = fireRate;
        this.isAiming = isAiming;
        this.bulletsAmount = bulletsAmount;
        this.bulletSpeed = bulletSpeed;
        
        this.startAngle = startAngle;
        this.endAngle = endAngle;
        
        PatternSwitch();
    }
    
    public void SetBulletPatternNone()
    {
        CancelInvoke("ConeCircle");
        activebulletPattern = BulletPatternEnum.BulletPatternsEnum.None;
    }

    public void PatternSwitch()
    {
        switch (activebulletPattern)
        {
            case BulletPatternEnum.BulletPatternsEnum.None:
                //Cooldown
                break;
            case BulletPatternEnum.BulletPatternsEnum.Circle:
                //Can Aim
                Invoke("ConeCircle", 0f);
                break;
            case BulletPatternEnum.BulletPatternsEnum.Cone:
                //Can Aim
                Invoke("ConeCircle", 0f);
                break;
            case BulletPatternEnum.BulletPatternsEnum.Straight:
                //Can Aim
                InvokeRepeating("ConeCircle", 0f, fireRate);
                break;
            case BulletPatternEnum.BulletPatternsEnum.Spiral:
                //Cannot Aim
                InvokeRepeating("SpiralFire", 0f, fireRate);
                break;
            case BulletPatternEnum.BulletPatternsEnum.DoubleSpiral:
                //Cannot Aim
                InvokeRepeating("DoubleSpiralFire", 0f, fireRate);
                break;
            case BulletPatternEnum.BulletPatternsEnum.Pyramid:
                //Can Aim
                InvokeRepeating("Pyramid", 0f, fireRate);
                break;
            case BulletPatternEnum.BulletPatternsEnum.WaveDecel:
                InvokeRepeating("WaveDecel", 0f, fireRate);
                break;
            case BulletPatternEnum.BulletPatternsEnum.WayAllRange:
                InvokeRepeating("WayAllRange", 0f, fireRate);
                break;
        }
    }

    private void ConeCircle()
    {
        Debug.Log("Cone Pattern");
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate the direction towards the player using Aimed() method
            Vector3 bulDir = isAiming ? (AimInstance.Aiming()) : AimInstance.RandomAim();

            // Rotate the bullet direction by the cone pattern angle around the y-axis
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            bulDir = rotation * bulDir;

            // Rotate the bullet direction around the x-axis
            float rotatedX = bulDir.x * Mathf.Cos(rotation.eulerAngles.x * Mathf.Deg2Rad) - bulDir.z * Mathf.Sin(rotation.eulerAngles.x * Mathf.Deg2Rad);
            float rotatedZ = bulDir.x * Mathf.Sin(rotation.eulerAngles.x * Mathf.Deg2Rad) + bulDir.z * Mathf.Cos(rotation.eulerAngles.x * Mathf.Deg2Rad);
            bulDir.x = rotatedX;
            bulDir.y = AimInstance.RandomAim().y;
            bulDir.z = rotatedZ;

            // Create a new vector with the rotated direction
            // bulDir.Normalize();

            // Spawn and set direction for the bullet
            GameObject bul = GetCorrectBullet();
            bul.transform.position = user.transform.position;
            bul.transform.rotation = user.transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            
            angle += angleStep;
        }
    }

    private GameObject GetCorrectBullet()
    {
        GameObject bul = null;
        if (user.tag == "Player")
        {
            bul = BulletPool.Instance.GetBulletPlayer();
            Debug.Log("Player Shoots");
        }
        else
        {
            bul = BulletPool.Instance.GetBulletEnemy();
            Debug.Log("Enemy Shoots");
        }

        return bul;
    }
    
}
