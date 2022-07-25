using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;
    public bool timerOn;
    public Feedback feedback;

    [SerializeField] Text countdownText;
    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString ("0");
        }
        
        if (currentTime <= 0)
        {
            currentTime = 0;
        }
        if (currentTime == 0)
        {
            SceneManager.LoadScene("GOPuzzle");
        }
        if(feedback.selesai)
        {
            timerOn = false;
        }
    }
}
