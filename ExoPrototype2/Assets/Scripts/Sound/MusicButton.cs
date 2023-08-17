using UnityEngine;

namespace Sound
{
    public class MusicButton : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip clip;
        
        public void SetAudioClip(AudioClip clip)
        {
            this.clip = clip;
        }
        
        public void PlayMusic()
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}