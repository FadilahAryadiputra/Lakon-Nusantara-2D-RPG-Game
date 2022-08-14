using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource bgbuttton;
    // int levelsUnlocked;
    // public Button[] buttons;

   

    public void LoadStageJateng()
    {
        SceneManager.LoadScene("SplashJateng");
        AudioManager.instance.buttonSFX();
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
        SceneManager.LoadScene("CutScene");
        AudioManager.instance.buttonSFX();
    }

    public void Peta()
    {
        SceneManager.LoadScene("Peta");
        AudioManager.instance.buttonSFX();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("OpenScene");
        AudioManager.instance.buttonSFX();
    }

    public void Close()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.buttonSFX();
    }

    public void QuitGame ()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
        AudioManager.instance.buttonSFX();
    }

    public void Play()
    {
        bgbuttton.Play();
    }

    public void retry()
    {
        SceneManager.LoadScene("Puzzle");
    }

    public void retryquiz()
    {
        DataQuiz.skor = 0;
        SceneManager.LoadScene("Quiz");
    }

    public void retrytebak()
    {
        Data.score = 0;
        SceneManager.LoadScene("Drag&Drop");
    }

    public void SelesaiPuzzle()
    {
        SceneManager.LoadScene("CutSceneJawa");
    }

    public void CSPapua()
    {
        SceneManager.LoadScene("CutScenePapua");
    }
}
