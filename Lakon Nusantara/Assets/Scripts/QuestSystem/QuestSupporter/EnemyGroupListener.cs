using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupListener : MonoBehaviour
{
    public PlayerController player;
    public NPC_Farmer NPC;
    public GameObject NPCDirectionBtn;
    public GameObject GoalDirectionBtn;
    public GameObject EnemyGroupParent;

    public bool allEnemyIsGone;
    public GameObject[] enemyOnGroup;

    private void FixedUpdate()
    {
        if(enemyOnGroup[0] == null && enemyOnGroup[1] == null && enemyOnGroup[2] == null &&
            enemyOnGroup[3] == null && enemyOnGroup[4] == null && enemyOnGroup[5] == null &&
            enemyOnGroup[6] == null && enemyOnGroup[7] == null && enemyOnGroup[8] == null)
        {
            allEnemyIsGone = true;
        } else {
            allEnemyIsGone = false;
        }
        
        if(player.quest.title != NPC.npcQuestTitle){
            NPCDirectionBtn.SetActive(true);
            GoalDirectionBtn.SetActive(false);
            EnemyGroupParent.SetActive(false);
        }
        if(player.quest.title == NPC.npcQuestTitle && player.quest.completed == false)
        {
            NPCDirectionBtn.SetActive(false);
            GoalDirectionBtn.SetActive(true);
            EnemyGroupParent.SetActive(true);
        }
        if(player.quest.title == NPC.npcQuestTitle && player.quest.completed == true){
            NPCDirectionBtn.SetActive(true);
            GoalDirectionBtn.SetActive(false);
            EnemyGroupParent.SetActive(false);
        }
        

        if(player.quest.title == NPC.npcQuestTitle && allEnemyIsGone == true)
        {
            // player.GroupKilled();
            Debug.Log("Amount");
            player.quest.goal.currentAmount = player.quest.goal.requiredAmount;
        }
    }
}
