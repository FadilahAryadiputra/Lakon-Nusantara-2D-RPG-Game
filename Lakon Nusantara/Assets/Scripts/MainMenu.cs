using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject areaDescPanel;
    public AudioSource bgbuttton;

    public void LoadSplashScreen()
    {
        SceneManager.LoadScene("SplashJateng");
    }

    public void OpenAreaDescPanel() {
        areaDescPanel.gameObject.SetActive(true);
    }

    public void CloseAreaDescPanel() {
        areaDescPanel.gameObject.SetActive(false);
    }





    public void LoadStageJateng()
    {
        SceneManager.LoadScene("SplashJateng");
        AudioController.instance.buttonSFX();
    }
    
    public void PlayGame ()
    {
        SceneManager.LoadScene("CutScene");
        AudioController.instance.buttonSFX();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("OpenScene");
        AudioController.instance.buttonSFX();
    }

    public void Close()
    {
        SceneManager.LoadScene("MainMenu");
        AudioController.instance.buttonSFX();
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
