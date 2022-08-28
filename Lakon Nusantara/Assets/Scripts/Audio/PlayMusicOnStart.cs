using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{
    public AudioClip musicClip;

    void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        AudioManager.instance.PlayMusic(musicClip);
    }
}
