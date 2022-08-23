using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherQuestListener : MonoBehaviour
{
    public PlayerController player;
    public NPC_Farmer NPC;
    public GameObject NPCDirectionBtn;
    public GameObject GoalDirectionBtn;
    public GameObject ItemGroupParent;
    public GameObject ObjectivesItemPrefab;
    public Transform[] ItemLocation;

    private void FixedUpdate()
    {
        if(player.quest.title != NPC.npcQuestTitle){
            NPCDirectionBtn.SetActive(true);
            ItemGroupParent.SetActive(false);
            if(GoalDirectionBtn) {
                GoalDirectionBtn.SetActive(false);
            }
        }
        if(player.quest.title == NPC.npcQuestTitle && player.quest.completed == false)
        {
            NPCDirectionBtn.SetActive(false);
            ItemGroupParent.SetActive(true);
            if(GoalDirectionBtn) {
                GoalDirectionBtn.SetActive(true);
            }
        }
        if(player.quest.title == NPC.npcQuestTitle && player.quest.completed == true){
            NPCDirectionBtn.SetActive(true);
            ItemGroupParent.SetActive(false);
            if(GoalDirectionBtn) {
                GoalDirectionBtn.SetActive(false);
            }
        }
    }
}
