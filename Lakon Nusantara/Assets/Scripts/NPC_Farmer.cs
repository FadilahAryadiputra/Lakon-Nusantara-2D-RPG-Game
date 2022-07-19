using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPC_Farmer : MonoBehaviour
{
        public GameObject dialoguePanel;
        public Text dialogueText;
        public string[] dialogue;
        private int index;
        public int dialougeIndexValue;

        public GameObject continueButton;
        public GameObject closeDialougeButton;
        public float wordSpeed;
        public bool playerIsClose;

        public bool npcQuest;
        public GameObject npcQuestButton;
        public int questIndexDialouge;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {

            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
        if(npcQuest)
        {
            withQuest();
        }
        // dialougeIndexValue = dialogue[index];
        if(dialogueText.text == dialogue[dialougeIndexValue])
        {
            continueButton.SetActive(false);
            closeDialougeButton.SetActive(true);
        }
    }

    public void withQuest()
    {
        if(dialogueText.text == dialogue[questIndexDialouge])
        {
            npcQuestButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char Letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += Letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        continueButton.SetActive(false);
        closeDialougeButton.SetActive(false);
        npcQuestButton.SetActive(false);
        
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}

