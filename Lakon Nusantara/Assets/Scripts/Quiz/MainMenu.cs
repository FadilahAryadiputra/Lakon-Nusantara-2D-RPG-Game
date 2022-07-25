using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject btnPapua;

    public void PlayGame ()
    {
        SceneManager.LoadScene("Peta");
    }

    public void LoadSplashScreen()
    {
        SceneManager.LoadScene("SplashJateng");
    }

    public void LoadStageJateng()
    {
        SceneManager.LoadScene("KotaJateng");
    }

    public void LoadStagePapua()
    {
        SceneManager.LoadScene("KotaPapua");
    }

    public void QuitGame ()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }

    public void RetryPuzzle()
    {
        SceneManager.LoadScene("Puzzle");
    }

    public void RetryQuiz()
    {
        SceneManager.LoadScene("Quiz");
    }

    public void RetryDrag()
    {
        Data.score = 0;
        SceneManager.LoadScene("Drag&Drop");
    }

    public void SelesaiBos()
    {
        SceneManager.LoadScene("Peta");
    }

    public void update()
    {
        if(DataQuiz.skor >= 30)
        {
            btnPapua.SetActive(true);
        }
    }
}
