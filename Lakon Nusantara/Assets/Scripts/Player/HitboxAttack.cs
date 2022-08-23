using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    public float damage = 30f;
    public float thrustPower = 5f;
    public float knockTime = 0.3f;

    public AudioClip EnemyHitSFX;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();

            if(enemy != null) {
                enemy.Health -= damage;
                AudioManager.instance.PlaySFX(EnemyHitSFX);
                enemy.enemyHealthBar.gameObject.SetActive(true);
                enemy.enemyHealthBar.SetHealth((int)enemy.Health);
                enemy.LockMovement();
                enemy.rb.isKinematic = false;
                enemy.rb.gravityScale = 0f;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrustPower;
                enemy.rb.AddForce(difference, ForceMode2D.Impulse);

                // Start the timer to stop knockback
                enemy.isKnockedTime = knockTime;
				enemy.knockTimer = 0f;
				enemy.isKnocked = true;
            }

            EnemyMarksmanController enemy2 = other.gameObject.GetComponent<EnemyMarksmanController>();
            if(enemy2 != null) {
                enemy2.Health -= damage;
                enemy2.enemyHealthBar.gameObject.SetActive(true);
                enemy2.enemyHealthBar.SetHealth((int)enemy2.Health);
                enemy2.LockAction();
                enemy2.rb.isKinematic = false;
                enemy2.rb.gravityScale = 0f;
                Vector2 difference = enemy2.transform.position - transform.position;
                difference = difference.normalized * thrustPower;
                enemy2.rb.AddForce(difference, ForceMode2D.Impulse);
                
                // Start the timer to stop knockback
                enemy2.isKnockedTime = knockTime;
				enemy2.knockTimer = 0f;
				enemy2.isKnocked = true;
            }
        }
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }

    public void RemoveEnemyWithParent() {
        Destroy(transform.parent.gameObject);
    }
}
