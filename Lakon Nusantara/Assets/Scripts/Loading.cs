using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public Transform insertLoadingBar;
    public string sceneToLoad;

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
            SceneManager.LoadScene(sceneToLoad);
        }
        insertLoadingBar.GetComponent<Image>().fillAmount = valueNow / 100;
    }
}
