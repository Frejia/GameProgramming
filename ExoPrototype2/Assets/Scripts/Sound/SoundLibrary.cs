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
        //Get all MP3 Music Clips from a Folder
        /*DirectoryInfo dir = new DirectoryInfo("Assets/2D/Audio");
        FileInfo[] info = dir.GetFiles("*.mp3");
        foreach (FileInfo f in info)
        {
            AudioClip clip = Resources.Load("Assets/2D/Audio/" + f.Name) as AudioClip;
            MusicClips.Add(clip);
        }*/
     //   PlayMusic(0);
        
        
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
