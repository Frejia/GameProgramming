using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell
{
    public class BulletPool : MonoBehaviour
    {
        public static BulletPool Instance { get; private set; }

        [SerializeField] private GameObject pooledBulletEnemy;
        [SerializeField] private GameObject pooledBulletPlayer1;
        [SerializeField] private GameObject pooledBulletPlayer2;
        private bool notEnoughBulletsInPool = true;
        
        private List<GameObject> bulletsEnemy;
        private List<GameObject> bulletsPlayer1;
        private List<GameObject> bulletsPlayer2;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            bulletsEnemy = new List<GameObject>();
            bulletsPlayer1 = new List<GameObject>();
            bulletsPlayer2 = new List<GameObject>();
        }

        public GameObject GetEnemyBulletPrefab()
        {
            return pooledBulletEnemy;
        }
        public GameObject GetPlayer1BulletPrefab()
        {
            return pooledBulletPlayer1;
        }
        
        public GameObject GetPlayer2BulletPrefab()
        {
            return pooledBulletPlayer2;
        }
        
        public GameObject GetBulletEnemy()
        {
            if (bulletsEnemy.Count > 0)
            {
                for (int i = 0; i < bulletsEnemy.Count; i++)
                {
                    if (!bulletsEnemy[i].activeInHierarchy)
                    {
                        return bulletsEnemy[i];
                    }
                }
            }
            if (notEnoughBulletsInPool)
            {
                GameObject bullet = Instantiate(pooledBulletEnemy);
                bullet.SetActive(false);
                bulletsEnemy.Add(bullet);
                return bullet;
            }
            return null;
        }
        
        public GameObject GetBulletPlayer1()
        {
            if (bulletsPlayer1.Count > 0)
            {
                for (int i = 0; i < bulletsPlayer1.Count; i++)
                {
                    if (!bulletsPlayer1[i].activeInHierarchy)
                    {
                        return bulletsPlayer1[i];
                    }
                }
            }
            if (notEnoughBulletsInPool)
            {
                GameObject bulletplayer = Instantiate(pooledBulletPlayer1);
                bulletplayer.SetActive(false);
                bulletsPlayer1.Add(bulletplayer);
                return bulletplayer;
            }
            return null;
        }
        
        public GameObject GetBulletPlayer2()
        {
            if (bulletsPlayer2.Count > 0)
            {
                for (int i = 0; i < bulletsPlayer2.Count; i++)
                {
                    if (!bulletsPlayer2[i].activeInHierarchy)
                    {
                        return bulletsPlayer2[i];
                    }
                }
            }
            if (notEnoughBulletsInPool)
            {
                GameObject bulletplayer = Instantiate(pooledBulletPlayer2);
                bulletplayer.SetActive(false);
                bulletsPlayer2.Add(bulletplayer);
                return bulletplayer;
            }
            return null;
        }
    }
}