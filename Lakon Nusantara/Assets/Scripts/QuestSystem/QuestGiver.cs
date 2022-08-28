using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public bool questWithTime;
    public QuestTimer questTimer;
    public PlayerController player;

    public GameObject objectivesPanel;
    public GameObject questWindow;
    public TMP_Text titleTMP;
    public TMP_Text descriptionTMP;
    public TMP_Text questTimeTMP;
    public TMP_Text timeStatusTMP;
    public GameObject notificationPanel;
    public TMP_Text notificationText;

    void Update()
    {
        if(questTimer.TimeIsUp == true)
        {
            quest.Failed();
        }
    }

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleTMP.text = quest.title;
        descriptionTMP.text = quest.description;
        if(questWithTime) {
            questTimeTMP.text = "Tenggat waktu : " + questTimer.TimeLeft + " detik";
        }
        if(questWithTime == false) {
            questTimeTMP.text = "Tanpa Tenggat Waktu";
        }
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void AcceptQuest()
    {
        if(player.quest.isActive == true)
        {
            notificationPanel.SetActive(true);
            StartCoroutine(NotificationTime());

            IEnumerator NotificationTime()
            {
                yield return new WaitForSeconds(5);
                notificationPanel.SetActive(false);
            }
        } else {
            if(questWithTime)
            {
                questWindow.SetActive(false);
                quest.isActive = true;
                player.quest = quest;
                player.questTimer = questTimer;
                objectivesPanel.gameObject.SetActive(true);
                timeStatusTMP.gameObject.SetActive(true);
                player.questTimer.TimerOn = true;
            }
            if(questWithTime == false)
            {
                questWindow.SetActive(false);
                quest.isActive = true;
                player.quest = quest;
                player.questTimer = questTimer;
                objectivesPanel.gameObject.SetActive(true);
                timeStatusTMP.gameObject.SetActive(false);
                player.questTimer.TimerOn = false;
            }
        }
    }
}
