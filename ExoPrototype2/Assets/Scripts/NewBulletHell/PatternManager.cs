using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using Unity.VisualScripting;
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

    void Awake()
    {
        //AimInstance = Aim.Instance;
        if (this.gameObject.tag == "Enemy")
        {
            AimInstance = this.gameObject.AddComponent<EnemyAim>();
            AimInstance.target = GameObject.FindGameObjectWithTag("Player");
            AimInstance.user = this.gameObject;
        }
        else if (this.gameObject.tag == "Player")
        {
            AimInstance = this.gameObject.GetComponent<PlayerAim>();
            AimInstance.user = this.gameObject;
        }
    }

    public void SetBulletPattern(BulletPatternEnum.BulletPatternsEnum bulletPattern, BulletHell.BulletBehaviour.BulletBehaviours bulletBehaviour, 
        float startAngle, float endAngle, float fireRate, bool isAiming, int bulletsAmount, float bulletSpeed)
    {
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
                InvokeRepeating("PwettyPattern", 0f, fireRate);
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
            bul.transform.position = this.gameObject.transform.position;
            //bul.transform.rotation = this.gameObject.transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            bul.GetComponent<BulletHell.BulletBehaviour>().SetBehaviour(activebulletBehaviour, bulDir);
            
            angle += angleStep;
        }
    }
    
     private float pyramidSize = 2f;

    // Pyramid Pattern
    public void Pyramid()
    {
        int numRows = Mathf.CeilToInt(Mathf.Sqrt(bulletsAmount));

        // Calculate the spacing between each row and column
        float rowSpacing = pyramidSize / (numRows - 1);
        float colSpacing = pyramidSize / 2f;
        int bulletsInBottomRow = 5;

        float rotationAngle = AimInstance.Aiming().x * Mathf.Rad2Deg;
        
        // Calculate the total number of bullets in the pyramid
        int totalBullets = 0;
        for (int row = 0; row < numRows; row++)
        {
            totalBullets += bulletsInBottomRow - row;
        }

        // Calculate the rotation angle for the entire pyramid
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        int bulletCount = 0;
        for (int row = 0; row < numRows; row++)
        {
            int bulletsInRow = bulletsInBottomRow - row;
            float xOffset = -(bulletsInRow - 1) * colSpacing * 0.5f;
            float zOffset = row * rowSpacing;

            for (int col = 0; col < bulletsInRow; col++)
            {
                float xPosition = col * colSpacing + xOffset;
                float zPosition = zOffset;

                Vector3 position = rotation * new Vector3(xPosition, AimInstance.RandomAim().y, zPosition) + transform.position; ;
                
                GameObject bul = BulletPool.Instance.GetBulletEnemy();
                bul.transform.position = position;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);
                bul.GetComponent<Bullet>().SetDirection((position - transform.position).normalized);
                bul.GetComponent<BulletHell.BulletBehaviour>().SetBehaviour(activebulletBehaviour, bulDir);
                
                bulletCount++;

                if (bulletCount >= totalBullets)
                {
                    // If we've spawned all bullets, exit the loop early
                    return;
                }
            }
        }
    }
    
    // Way all Range Pattern
    void WayAllRange()
    {
        float angle = 0;
        while (angle > -2 * Mathf.PI)
        {
            float bulDirX = transform.position.x + Mathf.Cos(angle) * 20;
            float bulDirZ = transform.position.z + Mathf.Sin(angle) * 20;
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirZ, 0f);
            bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.Instance.GetBulletEnemy();
            bul.transform.position = transform.position;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            bul.GetComponent<BulletHell.BulletBehaviour>().SetBehaviour(activebulletBehaviour, bulDir);
            angle = (float)(angle - Mathf.PI / 2f);
        }
    }

    //Single Spiral Pattern
    private void SpiralFire()
    {
        float angle = 0f;
        Vector3 bulDir = isAiming ? (AimInstance.Aiming()) : AimInstance.RandomAim();
        
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirZ = transform.position.z + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, 0f, bulDirZ);
        bulDir = (bulMoveVector - transform.position).normalized;

        GameObject bul = BulletPool.Instance.GetBulletEnemy();
        bul.transform.position = transform.position;
        bul.SetActive(true);
        bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);
        bul.GetComponent<Bullet>().SetDirection(bulDir);

        angle += 10f;
    }
    
    //Double Spiral Pattern
    private void DoubleSpiralFire()
    {
        float angle = 0f;
        
        for (int i = 0; i <= 1; i++)
        {
            Vector3 bulDir = isAiming ? (AimInstance.Aiming()) : AimInstance.RandomAim();
            
            float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
            float bulDirZ = transform.position.z + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);
            
            Vector3 bulMoveVector = new Vector3(bulDirX,  AimInstance.RandomAim().y, bulDirZ);
            bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.Instance.GetBulletEnemy();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
        }

        angle += 10f;

        if (angle >= 360f) angle = 0f;
    }
    
    private void PwettyPattern()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate the direction towards the player using Aimed() method
            Vector3 bulDir = isAiming ? (AimInstance.Aiming()) : AimInstance.RandomAim();

            // Rotate the bullet direction by the cone pattern angle
            float rotatedAngle = angle * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(rotatedAngle);
            float sinAngle = Mathf.Sin(rotatedAngle);

            // Rotate the direction vector
            float rotatedX = bulDir.x * cosAngle - bulDir.z * sinAngle;
            float rotatedZ = bulDir.x * sinAngle + bulDir.z * cosAngle;

            // Create a new vector with the rotated direction
            bulDir = new Vector3(rotatedX, AimInstance.RandomAim().y, rotatedZ);

            // Spawn and set direction for the bullet
            GameObject bul = BulletPool.Instance.GetBulletEnemy();
            bul.transform.position = transform.position;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            bul.GetComponent<BulletHell.BulletBehaviour>().SetBehaviour(BulletHell.BulletBehaviour.BulletBehaviours.SineCurve, bulDir);

            angle += angleStep;
        }
    }

    private GameObject GetCorrectBullet()
    {
        GameObject bul = null;
        if (this.gameObject.tag == "Player")
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
