using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    public float damage = 3;

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
            }
        }
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }
}
