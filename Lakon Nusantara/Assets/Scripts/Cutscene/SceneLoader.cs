using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneLoader : MonoBehaviour
{
    public FadeTransition fadeTransition;

    [SerializeField]
    private PlayableDirector introCutscene;
    [SerializeField]
    private double introSkipTime;

    // Start is called before the first frame update
    void Start()
    {
        fadeTransition.GoFadeScene01();
    }

    public void SkipIntro()
    {
        introCutscene.time = introSkipTime;
    }
}
