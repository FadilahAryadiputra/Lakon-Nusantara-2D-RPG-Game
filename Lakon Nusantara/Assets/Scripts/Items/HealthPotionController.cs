using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionController : MonoBehaviour
{
    public int healValue = 3;
    public GameObject HealParticlePrefab;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            PlayerController player = other.GetComponent<PlayerController>();

            Instantiate(HealParticlePrefab, transform.position, Quaternion.identity);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            AudioManager.instance.PlaySFX(audioManager.healSFX);
            player.currentHealth += healValue;
            player.healthBar.SetHealth((int)player.currentHealth);
            if(transform.parent != null) {
                RemoveEnemyWithParent();
            } else {
                Destroy(gameObject);
            }
        }
    }
    public void RemoveEnemyWithParent() {
        Destroy(transform.parent.gameObject);
    }
}
