using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public GameObject theDisplay;
    public int hour;
    public int minute;
    public int second;
    // Start is called before the first frame update
    void Start()
    {
        hour = System.DateTime.Now.Hour;
        minute = System.DateTime.Now.Minute;
        second = System.DateTime.Now.Second;
        theDisplay.GetComponent<Text>().text = hour + " : " + minute + " : " + second;
    }

    // Update is called once per frame
    void Update()
    {
        hour = System.DateTime.Now.Hour;
        minute = System.DateTime.Now.Minute;
        second = System.DateTime.Now.Second;
        theDisplay.GetComponent<Text>().text = hour + " : " + minute + " : " + second;
    }
}
