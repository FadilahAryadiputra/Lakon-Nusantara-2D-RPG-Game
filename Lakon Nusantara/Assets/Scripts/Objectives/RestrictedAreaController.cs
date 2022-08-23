using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RestrictedAreaController : MonoBehaviour
{
    PlayerController player;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public GameObject playerSpriteGO;
    public GameObject restrictedAreaWarningDialogue;
    public AIPath AIPath;
    public Transform targetDestination;
    public Transform safeAreaPos;
    public float disableMovementTime = 2;
    public float warningTime = 3;
    public bool isMoveToSafeArea;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = playerSpriteGO.GetComponent<SpriteRenderer>();
        animator = playerSpriteGO.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMoveToSafeArea)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position,
                safeAreaPos.transform.position, player.moveSpeed * Time.deltaTime);
            
            Vector3 posDifference = player.transform.position - safeAreaPos.transform.position;
            if(posDifference.x < 0) {
                spriteRenderer.flipX = false;
            } else if (posDifference.x > 0) {
                spriteRenderer.flipX = true;
            }
            if(player.transform.position == safeAreaPos.transform.position)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player") 
		{
            isMoveToSafeArea = true;
            StartCoroutine(DisableMovementTime());
            StartCoroutine(WarningTime());

            IEnumerator DisableMovementTime()
            {
                player.dustParticle.Play();
                player.canMove = false;
                yield return new WaitForSeconds(disableMovementTime);
                player.dustParticle.Stop();
                player.canMove = true;
                isMoveToSafeArea = false;
            }

            IEnumerator WarningTime()
            {
                DefeatedDialogue defeatedDialogue = restrictedAreaWarningDialogue.GetComponent<DefeatedDialogue>();
                restrictedAreaWarningDialogue.SetActive(true);
                yield return new WaitForSeconds(warningTime);
                restrictedAreaWarningDialogue.SetActive(false);
                defeatedDialogue.zeroText();
            }
        }
    }
}
