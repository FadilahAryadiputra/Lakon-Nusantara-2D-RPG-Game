using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneToLoad_1;
    public string sceneToLoad_2;
    public string sceneToLoad_3;
    public string restartScene;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs Deleted!");
        }
    }

    public void LoadScene_1()
    {
        SceneManager.LoadScene(sceneToLoad_1);
    }

    public void LoadScene_2()
    {
        SceneManager.LoadScene(sceneToLoad_2);
    }

    public void LoadScene_3()
    {
        SceneManager.LoadScene(sceneToLoad_3);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(restartScene);
        CoinScoreData.score = 0;
    }
}
