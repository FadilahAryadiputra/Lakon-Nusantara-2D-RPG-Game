using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneToLoad;
    public string restartScene;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(restartScene);
    }
}
