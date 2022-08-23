using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    public bool isInsidePatrolArea;
    public bool isPatrolling;
    public float moveSpeed = 5;

    public float WaitTime = 3;
    public float currentWaitTime;
    public bool onMove;
    public float maxMoveTime = 10;
    public float currentMaxMoveTime;

    public Vector2 targetPos;
    public GameObject patrolSpot;
    // public string patrolSpotGO_Name;
    private float xSpot;
    private float ySpot;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    // Start is called before the first frame update
    void Start()
    {
        currentWaitTime = WaitTime;
        currentMaxMoveTime = maxMoveTime;
        // patrolSpot = GameObject.Find(patrolSpotGO_Name);
        xSpot = patrolSpot.transform.position.x;
        ySpot = patrolSpot.transform.position.y;
        targetPos = new Vector2(Random.Range(xSpot + minX, xSpot + maxX), Random.Range(ySpot + minY, ySpot + maxY));
    }

    // Update is called once per frame
    void Update()
    {
        if(isPatrolling)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, targetPos) < 0.2f) {
                if(currentWaitTime <= 0) {
                    targetPos = new Vector2(Random.Range(xSpot + minX, xSpot + maxX), Random.Range(ySpot + minY, ySpot + maxY));
                    onMove = true;
                    currentWaitTime = WaitTime;
                    currentMaxMoveTime = maxMoveTime;
                } else {
                    currentWaitTime -= Time.deltaTime;
                    onMove = false;
                }
            }

            if(onMove == true)
            {
                if(currentMaxMoveTime <= 0)
                {
                    targetPos = new Vector2(Random.Range(xSpot + minX, xSpot + maxX), Random.Range(ySpot + minY, ySpot + maxY));
                    onMove = true;
                    currentWaitTime = WaitTime;
                    currentMaxMoveTime = maxMoveTime;
                } else {
                    currentMaxMoveTime -= Time.deltaTime;
                }
            } 
        }

        // Check if the object is inside the patrol area range
        if(transform.position.x <= xSpot + maxX &&
            transform.position.x >= xSpot + minX &&
            transform.position.y >= ySpot + minY &&
            transform.position.y >= ySpot + minY)
        {
            isInsidePatrolArea = true;
        } 
        else {
            isInsidePatrolArea = false;
        }
    }
}
