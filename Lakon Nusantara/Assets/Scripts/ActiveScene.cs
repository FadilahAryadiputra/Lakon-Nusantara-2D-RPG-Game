using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveScene : MonoBehaviour
{
   public MoveToNextLevel moveToNextLevel;

   void OnEnable()
   {
      moveToNextLevel.WinUnlockLevel();
      SceneManager.LoadScene("Peta");
      AudioManager audioManager = FindObjectOfType<AudioManager>();
      AudioManager.instance.PlayMusic(audioManager.menuBGM);
   }
}
