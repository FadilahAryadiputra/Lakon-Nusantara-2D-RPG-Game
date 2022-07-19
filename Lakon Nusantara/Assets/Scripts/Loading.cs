using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public Transform insertLoadingBar;

    [SerializeField]
    private float valueNow;
    [SerializeField]
    private float valueVelocity;

    // Update is called once per frame
    void Update()
    {
        if(valueNow < 100)
        {
            valueNow += valueVelocity * Time.deltaTime;
            Debug.Log((int)valueNow);
        }
        else
        {
            SceneManager.LoadScene("TestingKotaJateng");
        }
        insertLoadingBar.GetComponent<Image>().fillAmount = valueNow / 100;
    }
}
