using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefeatedDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueTxtMP;
    [TextArea(3,10)]
    public string[] dialogue;
    public int dialougeIndexValue;
    private int index;

    public GameObject restartButton;
    public GameObject mainMenuButton;
    public float wordSpeed;

    public AudioClip TypingSFX;

    void Update()
    {
        if(dialoguePanel.activeInHierarchy)
        {
            // zeroText();
        }
        else
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
        if(dialogueTxtMP.text == dialogue[index])
        {
            if(restartButton) {
                restartButton.SetActive(true);
            }
        }
        if(dialogueTxtMP.text == dialogue[dialougeIndexValue])
        {
            if(restartButton) {
                restartButton.SetActive(true);
            }
            if(mainMenuButton) {
                mainMenuButton.SetActive(true);
            }
        }
    }

    public void zeroText()
    {
        dialogueTxtMP.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char Letter in dialogue[index].ToCharArray())
        {
            dialogueTxtMP.text += Letter;
            AudioManager.instance.PlaySFX(TypingSFX);
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueTxtMP.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
}
