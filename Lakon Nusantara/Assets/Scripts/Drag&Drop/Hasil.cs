using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Hasil : MonoBehaviour
{
    public Text txScore;
    public Text txHighScore;
    int highscore;
    public GameObject Next;
    public Score score;
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("HS", 0);
        if (Data.score > highscore )
        {
            highscore = Data.score;
            PlayerPrefs.SetInt("HS", highscore);
        }

        txScore.text = "Scores: " + score.currentScore;
        txHighScore.text = "Highscores: " + highscore;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

}
