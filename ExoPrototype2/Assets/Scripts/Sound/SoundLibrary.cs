using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Handles Sound Effect playing when moving, shooting, getting hit, etc.
/// </summary>
public class SoundLibrary : MonoBehaviour
{
    [Header("Player Audio Clips")]
    [SerializeField] public List<AudioClip> EffectaudioClips;
    [SerializeField] public AudioSource playerAudioSource;
    [SerializeField] public AudioSource playerBulletAudioSource;

    // Get all events that can trigger a sound
    private void OnEnable()
    {
        //Events
        PlayerShoot.Shot += PlaySound;
        ShipMovement.boostInit += PlaySound;
        Health.PlayerHitSound += PlaySound;
        Health.EnemyHitSound += PlaySound;
        Health.PlayerDead += PlaySound;
        Health.EnemyDead += PlaySound;
        NewEnemyInView.OnEnemySeenSound += PlaySound;
    }
    
    private void OnDisable()
    {
        //Events
        PlayerShoot.Shot -= PlaySound;
        ShipMovement.boostInit -= PlaySound;
        Health.PlayerHitSound -= PlaySound;
        Health.EnemyHitSound -= PlaySound;
        Health.PlayerDead -= PlaySound;
        Health.EnemyDead -= PlaySound;
        NewEnemyInView.OnEnemySeenSound -= PlaySound;
    }

    // Play a sound on correct AudioSource
    private void PlaySound(int soundIndex)
    {
        if (soundIndex < 4)
        {
            playerBulletAudioSource.PlayOneShot(EffectaudioClips[soundIndex]);
        }else
        {
            playerAudioSource.PlayOneShot(EffectaudioClips[soundIndex]);
        }

    }
}