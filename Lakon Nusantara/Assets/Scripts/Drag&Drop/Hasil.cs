using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Hasil : MonoBehaviour
{
    public Text txScore;
    public Text txHighScore;
    int highscore;
    public GameObject Next;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject label1;
    public GameObject label2;
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("HS", 0);
        if (Data.score > highscore )
        {
            highscore = Data.score;
            PlayerPrefs.SetInt("HS", highscore);
        }
        if(Data.score <= 25)
        {
            Button2.SetActive(false);
        }
        else
        {
            Button1.SetActive(false);
            // Tanda.MapbtnPapua.activeInHierarchy(true);
        }

        txScore.text = "Scores: " + Data.score;
        txHighScore.text = "Highscores: " + highscore;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

}
