using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    [SerializeField] public List<AudioClip> audioClips;
    [SerializeField] public AudioSource playerAudioSource;

    private void OnEnable()
    {
        //Events
        PlayerShoot.Shot += PlaySound;
        PlayerShoot.ShotSpecial += PlaySound;
    }
    
    private void OnDisable()
    {
        //Events
        PlayerShoot.Shot -= PlaySound;
        PlayerShoot.ShotSpecial -= PlaySound;
    }


    private void PlaySound()
    {
        //Play the first sound of the List
        playerAudioSource.PlayOneShot(audioClips[0]);
    }
}
