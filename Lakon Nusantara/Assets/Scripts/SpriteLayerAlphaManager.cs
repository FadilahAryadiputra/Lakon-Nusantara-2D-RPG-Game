using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerAlphaManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
