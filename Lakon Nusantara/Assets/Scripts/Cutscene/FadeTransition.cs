using UnityEngine;
using System.Collections;

public class FadeTransition : MonoBehaviour {
    //name of the scene you want to load
    public string sceneToLoad01;
    public string sceneToLoad02;
    public string sceneToLoad03;
    public string restartScene;
    public float transitionSpeed = 1.0f;
	public Color loadToColor = Color.black;
	
	public void GoFadeScene01()
    {
        Initiate.Fade(sceneToLoad01, loadToColor, transitionSpeed);
    }
    public void GoFadeScene02()
    {
        Initiate.Fade(sceneToLoad02, loadToColor, transitionSpeed);
    }
    public void GoFadeScene03()
    {
        Initiate.Fade(sceneToLoad03, loadToColor, transitionSpeed);
    }
    public void RestartSceneFade()
    {
        Initiate.Fade(restartScene, loadToColor, transitionSpeed);
        CoinScoreData.score = 0;
    }
}
