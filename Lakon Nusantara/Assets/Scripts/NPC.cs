using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
        public GameObject dialoguePanel;
        public Text dialogueText;
        public string[] dialogue;
        private int index;

        public GameObject contButton;
        public float wordSpeed;
        public bool playerIsClose;

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        // {

        //     if(dialoguePanel.activeInHierarchy)
        //     {
        //         zeroText();
        //     }
        //     else
        //     {
        //         dialoguePanel.SetActive(true);
        //         StartCoroutine(Typing());
        //     }
        // }
        if(dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // { 
    //     if (collision.tag.Equals("Player") && playerIsClose)
    //     {
    //         if(dialoguePanel.activeInHierarchy)
    //         {
    //             zeroText();
    //         }
    //         else
    //         {
    //             dialoguePanel.SetActive(true);
    //             StartCoroutine(Typing());
    //         }
    //     }
        
    //     if(dialogueText.text == dialogue[index])
    //     {
    //         contButton.SetActive(true);
    //     }
    // }

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

        contButton.SetActive(false);
         
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
        if (other.tag.Equals("Player"))
        {
            playerIsClose = true;
        }
        if (playerIsClose == true)
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
        // if(dialogueText.text == dialogue[index])
        // {
        //     contButton.SetActive(true);
        // }
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

