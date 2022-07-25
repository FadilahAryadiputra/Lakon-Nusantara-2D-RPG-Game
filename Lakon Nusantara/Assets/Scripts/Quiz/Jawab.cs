using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jawab : MonoBehaviour
{
    public AudioSource benar;
    public AudioSource salah;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void jawaban(bool jawab)
    {
        if(jawab)
        {
            // int Skor = PlayerPrefs.GetInt("skor") + 10;
            // PlayerPrefs.SetInt("skor", Skor);
            DataQuiz.skor += 10;
            benar.Play();
            gameObject.SetActive(false);
            transform.parent.GetChild (gameObject.transform.GetSiblingIndex () + 1).gameObject.SetActive(true);
        }
        else
        {
            salah.Play();
            gameObject.SetActive(false);
            transform.parent.GetChild (gameObject.transform.GetSiblingIndex () + 1).gameObject.SetActive(true);
        }
        // gameObject.SetActive(false);
        // transform.parent.GetChild (gameObject.transform.GetSiblingIndex () + 1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
