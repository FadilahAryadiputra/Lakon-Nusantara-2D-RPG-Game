using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    // private BoxCollider2D bounds;
    // private CameraController theCamera;

    // void Start()
    // {
    //     bounds = GetComponent<BoxCollider2D>();
    //     theCamera = FindObjectOfType<CameraController>();
    //     theCamera.SetBounds(bounds);
    // }
    public Vector2 camMaxChange;
    public Vector2 camMinChange;

    public Vector3 PlayerChange;

    private CameraFollowing cam;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraFollowing>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            TransitionRoom(other);
        }
    }

    private void TransitionRoom(Collider2D other)
    {
        cam.minPos += camMinChange;
        cam.maxPos += camMaxChange;

        other.transform.position += PlayerChange;
    }


}
