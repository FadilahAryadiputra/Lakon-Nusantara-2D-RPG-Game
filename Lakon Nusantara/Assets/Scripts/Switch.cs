using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Image Day;
    public Image Night;
    // public GameObject Light;
    // public GameObject BonFire;
    public GameObject DayAsset;
    public GameObject BGDay;
    public GameObject BGNight;
    public GameObject EnemyDay;
    public GameObject EnemyNight;
    public GameObject PlayerDay;
    public GameObject PlayerNight;
    public GameObject PlayerPapuaDay;
    public GameObject PlayerPapuaNight;
    public GameObject NecroGuyNight;
    public GameObject NecroGuyDay;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(index == 1)
        {
            // Light.SetActive(false);
            // BonFire.SetActive(false);
            DayAsset.SetActive(true);
            BGDay.SetActive(true);
            BGNight.SetActive(false);
            EnemyDay.SetActive(true);
            EnemyNight.SetActive(false);
            PlayerDay.SetActive(true);
            PlayerNight.SetActive(false);
            PlayerPapuaDay.SetActive(true);
            PlayerPapuaNight.SetActive(false);
            NecroGuyNight.SetActive(false);
        }
        if(index == 0)
        {
            // Light.SetActive(true);
            // BonFire.SetActive(true);
            DayAsset.SetActive(false);
            BGDay.SetActive(false);
            BGNight.SetActive(true);
            EnemyDay.SetActive(false);
            EnemyNight.SetActive(true);
            PlayerDay.SetActive(false);
            PlayerNight.SetActive(true);
            PlayerPapuaDay.SetActive(false);
            PlayerPapuaNight.SetActive(true);
            NecroGuyNight.SetActive(true);
        }
    }

    public void day()
    {
        index = 1;
        Day.gameObject.SetActive(true);
        Night.gameObject.SetActive(false);
    }

    public void night()
    {
        index = 0;
        Day.gameObject.SetActive(false);
        Night.gameObject.SetActive(true);
    }
}
