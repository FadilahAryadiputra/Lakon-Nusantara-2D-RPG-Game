using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string itemName;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            PlayerController player = other.GetComponent<PlayerController>();
            player.ItemCollected();
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            AudioManager.instance.PlaySFX(audioManager.collectItemSFX);
            Destroy(gameObject);
        }
    }
}
