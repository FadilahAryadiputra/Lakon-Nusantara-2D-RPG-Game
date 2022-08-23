using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCoinScore : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<TMP_Text>().text = CoinScoreData.score.ToString("0");
    }

}
