using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{   
    public FadeTransition fadeTransition; 

    void Update()
    {
        if(Input.anyKeyDown)
        {
            fadeTransition.GoFadeScene01();
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            AudioManager.instance.PlaySFX(audioManager.hitButtonSFX);
        }
    }
}
