using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    public float damage = 30f;
    public float thrustPower = 5f;
    public float knockTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            // Deal damage to the enemy
            EnemyController enemy = other.GetComponent<EnemyController>();

            if(enemy != null) {
                enemy.Health -= damage;
                enemy.enemyHealthBar.gameObject.SetActive(true);
                enemy.enemyHealthBar.SetHealth((int)enemy.Health);
                enemy.LockMovement();
                enemy.rb.isKinematic = false;
                enemy.rb.gravityScale = 0f;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrustPower;
                enemy.rb.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo());
            }
            IEnumerator KnockCo() {
                if(enemy != null) {
                    yield return new WaitForSeconds(knockTime);
                    enemy.rb.velocity = Vector2.zero;
                    enemy.rb.isKinematic = true;
                    enemy.rb.velocity = Vector2.zero;
                    if(enemy.health >= 1) {
                        enemy.UnlockMovement();
                    }
                }
            }
        }
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }
}
