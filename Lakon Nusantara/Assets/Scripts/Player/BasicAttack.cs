using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public PlayerController player;
    public AttackOffsetController attackOffset;

    public void PlayBasicAttack() {
        player.LockMovement();
        player.currentStamina = player.currentStamina - player.basicAttackStaminaUsage;
        AudioManager.instance.PlaySFX(player.BasicAttackSFX);

        if(player.spriteRenderer.flipX == true){
            attackOffset.AttackLeft();
        } else {
            attackOffset.AttackRight();
        }
    }

    public void EndBasicAttack() {
        player.UnlockMovement();
    }
}
