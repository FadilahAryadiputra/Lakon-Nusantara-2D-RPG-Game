using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int currentScore;
    void FixedUpdate()
    {
        GetComponent<Text>().text = Data.score.ToString("0");
        currentScore = Data.score;
    }
}
