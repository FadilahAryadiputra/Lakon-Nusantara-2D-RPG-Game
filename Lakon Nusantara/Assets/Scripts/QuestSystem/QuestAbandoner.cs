using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAbandoner : MonoBehaviour
{
    [Header("Player Quest Abandoner")]
    public Quest questAbandoner;
    public QuestTimer questTimerAbandoner;
    [Header("NPC QuestGiver Reset")]
    public Quest questGiverReset;
    public QuestTimer questTimerGiverReset;
    public QuestGiver questGiver;
    public PlayerController player;
    public NPC_Farmer NPC;
    
    public void AbandonQuest()
    {
        if(player.quest.title == NPC.npcQuestTitle)
        {
            player.quest = questAbandoner;
            player.questTimer = questTimerAbandoner;
            questGiver.quest = questGiverReset;
            questGiver.questTimer = questTimerGiverReset;
            StartCoroutine(SwitchingDialogueDelay());
            
            IEnumerator SwitchingDialogueDelay()
            {
                yield return new WaitForSeconds(1f);
                NPC.dialogue1IsActive = true;
                NPC.dialogue2IsActive = false;
                NPC.dialogue3IsActive = false;
            }
        } else {
            Debug.Log("There's no quest to be abandoned!");
        }
    }
}
