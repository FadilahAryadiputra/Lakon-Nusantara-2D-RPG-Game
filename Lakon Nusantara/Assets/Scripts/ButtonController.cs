using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject areaDescPanel;

    public GameObject ObjectivesPanel;

    // -- Scene Peta --
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(areaDescPanel)
        {
            areaDescPanel.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(areaDescPanel)
        {
            areaDescPanel.SetActive(false);
        }
    }

    // -- Scene Level || Open Objectives Panel --
    public void OpenObjectivesPanel(){
        if(ObjectivesPanel)
        {
            if (ObjectivesPanel.gameObject.activeInHierarchy == false) {
                ObjectivesPanel.gameObject.SetActive(true);
            } else if (ObjectivesPanel.gameObject.activeInHierarchy == true) {
                ObjectivesPanel.gameObject.SetActive(false);
            }
        }
    }

    public void PlayHitButtonSFX()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        AudioManager.instance.PlaySFX(audioManager.hitButtonSFX);
    }

    public void StopMenuBGM() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        AudioManager.instance.StopMusic(audioManager.menuBGM);
    }

    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs Deleted!");
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}
