using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class SoundLibrary : MonoBehaviour
{
    [Header("Player Audio Clips")]
    [SerializeField] public List<AudioClip> EffectaudioClips;
    [SerializeField] public List<AudioClip> MusicClips;
    [SerializeField] public AudioSource playerAudioSource;
    [SerializeField] public AudioSource musicAudioSource;

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

    private void Start()
    {
        
        // find directory "assets/2d/audio"
        DirectoryInfo dir = new DirectoryInfo("Assets/2D/Audio");
        
        // get all mp3 files in directory
        FileInfo[] info = dir.GetFiles("*.mp3");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Name);
        }
        
        //TODO: implement Method that displays all audio files in the directory
        
    }

    private void PlaySound()
    {
        //Play the first sound of the List
        playerAudioSource.PlayOneShot(EffectaudioClips[0]);
    }
    
    private void PlayMusic(int index)
    {
        //Play the first sound of the List
        musicAudioSource.clip = MusicClips[0];
        musicAudioSource.Play();
    }
}