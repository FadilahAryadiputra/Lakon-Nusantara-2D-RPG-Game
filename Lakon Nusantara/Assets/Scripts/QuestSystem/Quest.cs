using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    private PlayerController player;

    public bool isActive;
    public bool completed;

    public string title;
    public string description;

    public int questID;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        player.questAchievement.questIndex[questID] = true;
        AudioManager.instance.PlaySFX(player.objectivesSuccessSFX);
        Debug.Log(title + " telah selesai!");
    }

    public void Failed()
    {
        isActive = false;
        Debug.Log("Misi " + title + " gagal diselesaikan!");
    }
}
