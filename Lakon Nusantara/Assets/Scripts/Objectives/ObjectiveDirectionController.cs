using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ObjectiveDirectionController : MonoBehaviour
{
    PlayerController player;
    public AIPath AIPath;
    public Transform targetDestination;

    public GameObject objectives01Panel;
    public GameObject objectives02Panel;
    public GameObject objectives03Panel;
    public GameObject objectivesMiniGamePanel;

    [Header("Objective Position")]
    public Transform objectives01Pos;
    public Transform objectives01GoalPos;
    public Transform objectives02Pos;
    public Transform objectives02GoalPos;
    public Transform objectives03Pos;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(objectives01Panel) {
            if(player.questAchievement.questIndex[0] == true) {
                objectives01Panel.SetActive(false);
            } else {
                objectives01Panel.SetActive(true);
            }
        }
        if(objectives02Panel) {
            if(player.questAchievement.questIndex[1] == true) {
                objectives02Panel.SetActive(false);
            } else {
                objectives02Panel.SetActive(true);
            }
        }
        if(objectives03Panel) {
            if(player.questAchievement.questIndex[2] == true) {
                objectives03Panel.SetActive(false);
            } else {
                objectives03Panel.SetActive(true);
            }
        }

        if(player.allQuestCompleted == true)
        {
            objectivesMiniGamePanel.SetActive(true);
        } else {
            objectivesMiniGamePanel.SetActive(false);
        }
    }

    public void GoToObjective01Pos() {
        targetDestination.position = objectives01Pos.position;
        StartPathfinding();
    }
    public void GoToObjective01GoalPos() {
        targetDestination.position = objectives01GoalPos.position;
        StartPathfinding();
    }
    public void GoToObjective02Pos() {
        targetDestination.position = objectives02Pos.position;
        StartPathfinding();
    }
    public void GoToObjective02GoalPos() {
        targetDestination.position = objectives02GoalPos.position;
        StartPathfinding();
    }
    public void GoToObjective03Pos() {
        targetDestination.position = objectives03Pos.position;
        StartPathfinding();
    }

    private void StartPathfinding() {
        if (player.defeated != true) {
            AIPath.canSearch = true;
            AIPath.canMove = true;
            player.dustParticle.Play();
            AudioManager.instance.PlaySFXLoop(player.PlayerWalkSFX);
            // PathfindingManager pathfindingManager = FindObjectOfType<PathfindingManager>();
            // pathfindingManager.CheckUpdateStopFX();
        }
    }
}
