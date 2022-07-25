using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public bool questWithTime;
    public QuestTimer questTimer;
    public PlayerController player;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text questTimeText;
    public Text timeStatusText;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        // TimeLeftText.text = "Tenggat waktu = : " + quest.QuestTimer;
        questTimeText.text = "Tenggat waktu = : " + questTimer.TimeLeft;
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void AcceptQuest()
    {
        if(questWithTime)
        {
            questWindow.SetActive(false);
            quest.isActive = true;
            player.quest = quest;
            player.questTimer = questTimer;
            timeStatusText.gameObject.SetActive(true);
            player.questTimer.TimerOn = true;
        }
        if(questWithTime == false)
        {
            questWindow.SetActive(false);
            quest.isActive = true;
            player.quest = quest;
        }
    }
}
