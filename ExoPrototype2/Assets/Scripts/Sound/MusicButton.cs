using System.Diagnostics;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// Used for the MusicButtonPrefab
    /// Set the music clip to play when the button is pressed.
    ///
    /// Referenced by MusicLoader
    /// </summary>
    public class MusicButton : MonoBehaviour
    {
        [Header("Muic")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip clip;
        
        public void SetAudioClip(AudioClip clip)
        {
            this.clip = clip;
        }
        
        // Play the music clip when button pressed
        public void PlayMusic()
        {
            musicSource = GameObject.Find("MusicAudioSource").GetComponent<AudioSource>();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}