using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    // public int nextSceneLoad;

    public string nextLevel = "Level2Papua";
    public int levelToUnlock = 3;       //Index number of the scene on build settings that will be unlocked

    public void WinUnlockLevel()
    {
        Debug.Log("Level Won!");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }
}
