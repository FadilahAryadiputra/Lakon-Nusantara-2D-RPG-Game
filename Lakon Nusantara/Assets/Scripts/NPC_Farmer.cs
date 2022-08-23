using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class NPC_Farmer : MonoBehaviour
{
        public GameObject TalkIcon;
        public GameObject TalkBtn;

        public GameObject dialoguePanel;
        public TMP_Text dialogueTxtMP;
        public string[] dialogue1;
        public string[] dialogue2;
        public string[] dialogue3;
        public int index1;
        public int index2;
        public int index3;
        public int dialouge1IndexValue;
        public int dialouge2IndexValue;
        public int dialouge3IndexValue;

        public GameObject NPCUniDialogueButton;
        public GameObject continueButton;
        public GameObject closeDialougeButton;
        public GameObject turnInQuestButton;
        public GameObject abandonQuestButton;
        public GameObject objectivesSuccessNotification;
        public GameObject objectivesFailedNotification;
        public QuestAbandoner questAbandoner;
        public float wordSpeed;
        public bool playerIsClose;

        public PlayerController player;
        public bool npcQuest;
        public string npcQuestTitle;
        public GameObject npcQuestButton;
        public int questIndexDialouge;

        public bool dialogue1IsActive = false;
        public bool dialogue2IsActive = false;
        public bool dialogue3IsActive = false;

        public AudioClip TypingSFX;
        public GameObject miniGameUnlockedDialogue;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            Interacted();
        }
        if(playerIsClose && dialogueTxtMP.text == dialogue1[index1] || 
            dialogueTxtMP.text == dialogue2[index2] || 
            dialogueTxtMP.text == dialogue3[index3])
        {
            if(NPCUniDialogueButton && playerIsClose)
            {
                NPCUniDialogueButton.SetActive(true);
                continueButton.SetActive(true);
            } else if(playerIsClose) {
                continueButton.SetActive(true);
            }
        }
        if(npcQuest)
        {
            withQuest();
        }
        if(dialogueTxtMP.text == dialogue1[dialouge1IndexValue])
        {
            continueButton.SetActive(false);
            closeDialougeButton.SetActive(true);
            turnInQuestButton.SetActive(false);
            abandonQuestButton.SetActive(false);
        }
        if(dialogueTxtMP.text == dialogue2[0])
        {
            continueButton.SetActive(true);
            closeDialougeButton.SetActive(false);
            turnInQuestButton.SetActive(false);
            abandonQuestButton.SetActive(true);
        }
        if(dialogueTxtMP.text == dialogue2[dialouge2IndexValue])
        {
            continueButton.SetActive(false);
            closeDialougeButton.SetActive(true);
            turnInQuestButton.SetActive(false);
            abandonQuestButton.SetActive(false);
        }
        if(dialogueTxtMP.text == dialogue3[dialouge3IndexValue] && npcQuest == true)
        {
            continueButton.SetActive(false);
            closeDialougeButton.SetActive(false);
            turnInQuestButton.SetActive(true);
            abandonQuestButton.SetActive(false);
        }
        if(dialogueTxtMP.text == dialogue3[dialouge3IndexValue] && npcQuest == false)
        {
            continueButton.SetActive(false);
            closeDialougeButton.SetActive(true);
            turnInQuestButton.SetActive(false);
            abandonQuestButton.SetActive(false);
        }

        if(npcQuest && player.quest.isActive == false && player.quest.title == npcQuestTitle)
        {
            dialogue1IsActive = true;
            dialogue2IsActive = false;
            dialogue3IsActive = false;
        } else if(npcQuest && player.quest.isActive == true && player.quest.completed == false && player.quest.title == npcQuestTitle) {
            dialogue1IsActive = false;
            dialogue2IsActive = true;
            dialogue3IsActive = false;
        } else if(npcQuest && (player.quest.completed == true && player.quest.title == npcQuestTitle)) {
            dialogue1IsActive = false;
            dialogue2IsActive = false;
            dialogue3IsActive = true;
        } else if(!npcQuest) {
            dialogue1IsActive = false;
            dialogue2IsActive = false;
            dialogue3IsActive = true;
        }

        if(player.questTimer.TimeIsUp == true && npcQuest == true)
        {
            questAbandoner.AbandonQuest();
            StartCoroutine(QuestFailedNotificationTime());
            
            IEnumerator QuestFailedNotificationTime()
            {
                objectivesFailedNotification.SetActive(true);
                yield return new WaitForSeconds(3);
                objectivesFailedNotification.SetActive(false);
            }
        }
    }

    public void Interacted()
    {
        if(dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
        else
        {
            dialoguePanel.SetActive(true);
            if(dialogue1IsActive == true && dialogue2IsActive == false && dialogue3IsActive == false) {
                StartCoroutine("Typing1");
            }
            if(dialogue1IsActive == false && dialogue2IsActive == true && dialogue3IsActive == false) {
                StartCoroutine("Typing2");
            }
            if(dialogue1IsActive == false && dialogue2IsActive == false && dialogue3IsActive == true) {
                StartCoroutine("Typing3");
            }
        }
    }

    public void withQuest()
    {
        if(dialogueTxtMP.text == dialogue1[questIndexDialouge])
        {
            npcQuestButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        StopCoroutine("Typing1");
        StopCoroutine("Typing2");
        StopCoroutine("Typing3");
        dialogueTxtMP.text = "";
        index1 = 0;
        index2 = 0;
        index3 = 0;
        dialoguePanel.SetActive(false);
        continueButton.SetActive(false);
        closeDialougeButton.SetActive(false);
        npcQuestButton.SetActive(false);
        abandonQuestButton.SetActive(false);
        turnInQuestButton.SetActive(false);
        if(NPCUniDialogueButton) {
            NPCUniDialogueButton.SetActive(false);
        }
    }

    IEnumerator Typing1()
    {
        foreach(char Letter in dialogue1[index1].ToCharArray())
        {
            dialogueTxtMP.text += Letter;
            AudioManager.instance.PlaySFX(TypingSFX);
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    IEnumerator Typing2()
    {
        foreach(char Letter in dialogue2[index2].ToCharArray())
        {
            dialogueTxtMP.text += Letter;
            AudioManager.instance.PlaySFX(TypingSFX);
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    IEnumerator Typing3()
    {
        foreach(char Letter in dialogue3[index3].ToCharArray())
        {
            dialogueTxtMP.text += Letter;
            AudioManager.instance.PlaySFX(TypingSFX);
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        closeDialougeButton.SetActive(false);
        npcQuestButton.SetActive(false);
        abandonQuestButton.SetActive(false);
        turnInQuestButton.SetActive(false);
        if(NPCUniDialogueButton) {
            NPCUniDialogueButton.SetActive(false);
        }

        if(index1 < dialogue1.Length - 1)
        {
            if(dialogue1IsActive == true && dialogue2IsActive == false && dialogue3IsActive == false)
            {
                if(index1 < dialogue1.Length - 1)
                {
                    index1++;
                    dialogueTxtMP.text = "";
                    StartCoroutine("Typing1");
                }
            }
        }
        else
        {
            zeroText();
        }
        if(index2 < dialogue2.Length -1)
        {
            if(dialogue1IsActive == false && dialogue2IsActive == true && dialogue3IsActive == false)
            {
                if(index2 < dialogue2.Length - 1)
                {
                    index2++;
                    dialogueTxtMP.text = "";
                    StartCoroutine("Typing2");
                }
            }
        }
        else
        {
            zeroText();
        }
        if(index3 < dialogue3.Length -1)
        {
            if(dialogue1IsActive == false && dialogue2IsActive == false && dialogue3IsActive == true)
            {
                if(index3 < dialogue3.Length - 1)
                {
                    index3++;
                    dialogueTxtMP.text = "";
                    StartCoroutine("Typing3");
                }
            }
        }
        else
        {
            zeroText();
        }
    }

    public void TurnInQuest()
    {
        zeroText();
        player.quest.Complete();
        player.quest.completed = false;
        npcQuest = false;
        StartCoroutine(QuestSuccessNotificationTime());
            
        IEnumerator QuestSuccessNotificationTime()
        {
            objectivesSuccessNotification.SetActive(true);
            yield return new WaitForSeconds(3);
            objectivesSuccessNotification.SetActive(false);
            yield return new WaitForSeconds(3);
            if(player.allQuestCompleted) {
                if(miniGameUnlockedDialogue)
                    miniGameUnlockedDialogue.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            TalkIcon.SetActive(true);
            TalkBtn.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
            TalkBtn.SetActive(false);
            TalkIcon.SetActive(false);
            if(NPCUniDialogueButton) {
                NPCUniDialogueButton.SetActive(false);
            }
        }
    }
}

