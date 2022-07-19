using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    Vector2 rightAttackOffset;

    public GameObject hitAreaRight;
    public Vector2 hitAreaRightOffset;
    public GameObject hitAreaLeft;
    public Vector2 hitAreaLeftOffset;

    private void Start() {
        rightAttackOffset = transform.position;
    }

    public void AttackRight() {
        transform.localPosition = rightAttackOffset;
        GameObject hitPoint = Instantiate(hitAreaRight, (Vector2)transform.position - hitAreaRightOffset * transform.localScale.x, Quaternion.identity);
    }

    public void AttackLeft() {
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
        GameObject hitPoint = Instantiate(hitAreaLeft, (Vector2)transform.position - hitAreaLeftOffset * transform.localScale.x, Quaternion.identity);
    }
}
