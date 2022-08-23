using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    // -- Make sure the waypoint's parrent position is 0 --

    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private int waypointIndex = 0;

    private void Start () {

        // Set position of this GameObject as position of the first waypoint
        // transform.position = waypoints[waypointIndex].transform.position;
	}

    private void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        // If this GameObject didn't reach last waypoint it can move
        // If this GameObject reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move this GameObject from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            // If this GameObject reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and this GameObject starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}