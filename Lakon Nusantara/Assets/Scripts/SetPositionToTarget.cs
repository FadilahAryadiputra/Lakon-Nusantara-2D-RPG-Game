using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionToTarget : MonoBehaviour
{
    public Vector3 targetPos;
    [SerializeField]
    private Vector3 offsetDiff;     // Dont forget to set offsetDiff
    [SerializeField]
    private GameObject child;

    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = child.transform.position - offsetDiff;
        transform.position = targetPos;
        child.transform.position = transform.position + offsetDiff;
    }
}
