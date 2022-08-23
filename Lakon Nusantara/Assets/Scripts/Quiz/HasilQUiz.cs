using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasilQUiz : MonoBehaviour
{
    public Text txtScore;
    public Text txtHighScore;
    int highskor;
    public GameObject Retry;
    public GameObject Done;
    public GameObject Note;
    // Start is called before the first frame update
    void Start()
    {
        highskor = PlayerPrefs.GetInt("HS", 0);
        if(DataQuiz.skor > highskor)
        {
            highskor = DataQuiz.skor;
            PlayerPrefs.SetInt("HS", highskor);
        }
        if(DataQuiz.skor <=30)
        {
            Done.SetActive(false);
            Note.SetActive(true);
        }
        else
        {
            Retry.SetActive(false);
            Note.SetActive(false);
        }
        txtScore.text = "Skor: " + DataQuiz.skor;
        txtHighScore.text = "Highscore: " + highskor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
