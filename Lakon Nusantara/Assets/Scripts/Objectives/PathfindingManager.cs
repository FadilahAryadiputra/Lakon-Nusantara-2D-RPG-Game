using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfindingManager : MonoBehaviour
{
    PlayerController player;
    public AIPath AIPath;

    public Vector3 findpathVelocityMonitor;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        AIPath.canSearch = false;
    }

    private void Update()
    {
        findpathVelocityMonitor = AIPath.velocity;

        if(AIPath.canMove == true) {
            player.animator.SetBool("isMoving", true);

            if(AIPath.desiredVelocity.x < 0.0f) {
                player.spriteRenderer.flipX = true;
            } else if (AIPath.desiredVelocity.x > 0.0f) {
                player.spriteRenderer.flipX = false;
            }

            if(AIPath.remainingDistance <= 1)
            {
                player.dustParticle.Stop();
                AudioManager.instance.StopSFXLoop(player.PlayerWalkSFX);
                AIPath.canSearch = false;
                Debug.Log("remainingDistance <= 1 while canMove is true");
            }
        }

        if(player.movementInput != Vector2.zero) {
            AIPath.canMove = false;
            AIPath.canSearch = false;
        } else if (player.canMove == false) {
            AIPath.canMove = false;
            AIPath.canSearch = false;
            player.dustParticle.Stop();
            AudioManager.instance.StopSFXLoop(player.PlayerWalkSFX);
        }
    }

    // public void CheckUpdateStopFX()
    // {
    //     StartCoroutine(CheckVelocityChanges());

    //     IEnumerator CheckVelocityChanges()
    //     {
    //         int loopNum = 180;
    //         for(int i = 0; i < loopNum; i++)
    //         {
    //             Vector3 startVel = AIPath.velocity;
    //             yield return new WaitForSeconds(0.75f);
    //             Vector3 finalVel = AIPath.velocity;
    //             if(startVel.x == finalVel.x && startVel.y == finalVel.y) {
    //                 player.dustParticle.Stop();
    //                 AudioManager.instance.StopSFXLoop(player.PlayerWalkSFX);
    //                 AIPath.canSearch = false;
    //                 Debug.Log("Velocity did not changes over 0.75 second!");
    //                 StopCoroutine("CheckVelocityChanges");
    //             } else if (startVel.x != finalVel.x && startVel.y != finalVel.y) {
    //                 Debug.Log("Velocity changes over 0.75 second!");
    //             }
    //         }
    //     }
    // }
}
