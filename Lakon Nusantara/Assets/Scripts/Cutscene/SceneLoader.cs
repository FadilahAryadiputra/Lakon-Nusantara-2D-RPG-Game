using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private PlayableDirector introCutscene;
    [SerializeField]
    private double introSkipTime;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SkipIntroJawa()
    {
        introCutscene.time = introSkipTime;
    }
}
