using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // int levelsUnlocked;
    // public Button[] buttons;

   

    public void LoadStageJateng()
    {
        SceneManager.LoadScene("SplashJateng");
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked",1);

    //     for (int i = 0; i < buttons.Length; i++)
    //     {
    //         buttons[i].interactable = false;
    //     }

    //     for (int i = 0; i < levelsUnlocked; i++)
    //     {
    //         buttons[i].interactable = true;
    //     }
    // }

    // public void LoadLevel(int levelIndex)
    // {
    //     SceneManager.LoadScene(levelIndex);
    // }

    public void PlayGame ()
    {
        SceneManager.LoadScene("Peta");
    }

    public void QuitGame ()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}
