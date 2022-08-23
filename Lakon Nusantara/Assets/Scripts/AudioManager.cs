using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource sfxLoopedSource;
    [Header("Audio Setting")]
    public AudioClip menuBGM;
    public AudioClip jatengBGM;
    public AudioClip hitButtonSFX;
    public AudioClip typingSFX;
    public AudioClip collectItemSFX;
    public AudioClip healSFX;

    private void Awake() {
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXLoop(AudioClip sfxLooped)
    {
        sfxLoopedSource.clip = sfxLooped;
        sfxLoopedSource.Play();
    }

    public void StopSFXLoop(AudioClip sfxLooped)
    {
        sfxLoopedSource.clip = sfxLooped;
        sfxLoopedSource.Stop();
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void StopMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Stop();
    }
}
