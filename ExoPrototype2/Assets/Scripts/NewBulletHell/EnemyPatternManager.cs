using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatternManager : MonoBehaviour
{
    PatternManager fireBullets;
    [SerializeField] private List<BulletPatterns> patterns;
    [SerializeField] private float[] patternDurations;
    public bool useAlternateDurations;
    //[SerializeField] private Collider ProximityField;
    public float Cooldown;
    private bool isOnCooldown;
    public float patternDuration;
    public bool isFiring = false;
    public bool isPlayerClose = false;
    private BulletPool pool;
    
    // Start is called before the first frame update
    void Start()
    {
        fireBullets = this.gameObject.GetComponent<PatternManager>();
        pool = BulletPool.Instance;
        EnemySeesPlayer.CanSee += PlayerClose;
        EnemySeesPlayer.CantSee += StopPatterns;
       //StartFiringPatterns();
        // StartFiringPatterns();
        //Start a Coroutine of StartPattern filling in a bulletPattern
    }
    
    private void StartFiringPatterns()
    {
        if (!isFiring && !isOnCooldown)
        {
            StartCoroutine(ReadBulletPatterns());
        }
    }

    private void PlayerClose(GameObject enemy)
    {
        if (!isPlayerClose)
        {
            enemy.GetComponent<EnemyPatternManager>().isPlayerClose = true;
            enemy.GetComponent<EnemyPatternManager>().StartFiringPatterns();
        }
    }

    private IEnumerator ReadBulletPatterns()
    {
        while (isPlayerClose)
        {
            foreach (BulletPatterns pattern in patterns)
            {
                if (pattern.patternType == BulletPatternEnum.BulletPatternsEnum.None)
                {
                    StopCoroutine(StartPattern(pattern));
                    isFiring = false;
                    if (useAlternateDurations) patternDuration = patternDurations[patterns.IndexOf(pattern)];
                    else patternDuration = pattern.patternDuration;
                    Cooldown = pattern.Cooldown;
                    fireBullets.SetBulletPatternNone();
                    yield return new WaitForSeconds(pattern.patternDuration);
                }
                else
                {
                    if (!isFiring)
                    {
                        StartCoroutine(StartPattern(pattern));
                    }
                }

                if (Cooldown > 0f)
                {
                    // Set the isOnCooldown flag to true and start the cooldown timer
                    isOnCooldown = true;
                    isFiring = false;
                    yield return new WaitForSeconds(Cooldown);
                    isOnCooldown = false;
                }
            }
        }

        Debug.Log("End of Patterns reached");
            isFiring = false;
            StopCoroutine(ReadBulletPatterns());

            isFiring = false;
    }

    private IEnumerator StartPattern(BulletPatterns pattern)
    {
        isFiring = true;
        if (useAlternateDurations) patternDuration = patternDurations[patterns.IndexOf(pattern)];
        else  patternDuration = pattern.patternDuration;
       // BulletPool.Instance.GetEnemyBulletPrefab().GetComponent<Bullet>().SetSpeed(pattern.BulletSpeed);
        Cooldown = pattern.Cooldown;
        fireBullets.SetBulletPattern(pattern.patternType, pattern.bulletBehaviour, pattern.startAngle, pattern.endAngle, 
            pattern.FireRate, pattern.isAiming, pattern.bulletAmount, pattern.BulletSpeed);
        yield return new WaitForSeconds(pattern.patternDuration);
        fireBullets.SetBulletPatternNone();
    }

    private void StopPatterns(GameObject enemy)
    {
        enemy.GetComponent<EnemyPatternManager>().StartFiringPatterns();
        StopCoroutine(enemy.GetComponent<EnemyPatternManager>().ReadBulletPatterns());
        enemy.GetComponent<EnemyPatternManager>().isFiring = false;
        enemy.GetComponent<EnemyPatternManager>().isPlayerClose = false;
    }
}
