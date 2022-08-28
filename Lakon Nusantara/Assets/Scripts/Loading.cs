using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public FadeTransition fadeTransition;
    public Transform insertLoadingBar;

    [SerializeField]
    private float valueNow;
    [SerializeField]
    private float valueVelocity;

    // Update is called once per frame
    void Update()
    {
        if(valueNow < 130)
        {
            valueNow += valueVelocity * Time.deltaTime;
        }
        else
        {
            fadeTransition.GoFadeScene01();
        }
        insertLoadingBar.GetComponent<Image>().fillAmount = valueNow / 100;
    }
}
