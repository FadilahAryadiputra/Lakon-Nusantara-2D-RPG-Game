using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;

    private void Update() {
        Vector2 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // transform.rotation = Quarternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
