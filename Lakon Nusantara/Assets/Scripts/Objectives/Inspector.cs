using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
    PlayerController player;
    GameObject child;
    public GameObject inspectPanel;
    public GameObject inspectButton;
    public int unlockInspectQuestID;
    public int warningMarkChildIndex;
    public bool canBeInspected;
    public bool playerIsClose;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        child = transform.GetChild(warningMarkChildIndex).gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            Inspected();
        }

        if(player.questAchievement.questIndex[unlockInspectQuestID] == true)
        {
            canBeInspected = true;
        } else {
            canBeInspected = false;
        }
        if(canBeInspected)
        {
            child.gameObject.SetActive(true);
        } else {
            child.gameObject.SetActive(false);
        }
    }

    public void Inspected()
    {
        if(canBeInspected)
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            AudioManager.instance.PlaySFX(audioManager.hitButtonSFX);
            if(inspectPanel.activeInHierarchy) {
                inspectPanel.gameObject.SetActive(false);
            } else {
                inspectPanel.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            if(canBeInspected) {
                inspectButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            inspectButton.SetActive(false);
        }
    }
}
