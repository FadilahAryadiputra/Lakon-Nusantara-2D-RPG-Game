using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{
    public AudioClip menuBGM;
    public AudioClip jatengBGM;

    void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        
        if(menuBGM) {
            AudioManager.instance.PlayMusic(menuBGM);
        }
        if(jatengBGM) {
            AudioManager.instance.PlayMusic(jatengBGM);
        }
    }
}
