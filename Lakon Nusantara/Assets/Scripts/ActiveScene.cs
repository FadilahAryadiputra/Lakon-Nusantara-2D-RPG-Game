using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveScene : MonoBehaviour
{
   public MoveToNextLevel moveToNextLevel;
   public FadeTransition fadeTransition;

   void OnEnable()
   {
      moveToNextLevel.WinUnlockLevel();
      fadeTransition.GoFadeScene01();
      AudioManager audioManager = FindObjectOfType<AudioManager>();
      AudioManager.instance.PlayMusic(audioManager.menuBGM);
   }
}
