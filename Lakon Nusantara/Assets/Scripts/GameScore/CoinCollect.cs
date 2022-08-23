using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinCollect : MonoBehaviour
{
    public float speed;
    public int coinScore = 100;

    public Transform target;
    public GameObject coinPrefab;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    public void StartCoinMove(Vector3 _intial, Action onComplete)
    {
        Vector3 targetPos = cam.ScreenToWorldPoint (new Vector3 (target.position.x, target.position.y, cam.transform.position.z * -1));
        GameObject _coin = Instantiate (coinPrefab, transform);

        // StartCoroutine(MoveCoin (_coin.transform, _intial, targetPos, onComplete));
        StartCoroutine(MoveCoinTrans(_coin.transform, _intial, target, onComplete));
    }

    // IEnumerator MoveCoin (Transform obj, Vector3 startPos, Vector3 endPos, Action onComplete)
    // {
    //     float time = 0;

    //     while (time < 1)
    //     {
    //         time += speed * Time.deltaTime;
    //         obj.position = Vector3.Lerp (startPos, endPos, time);

    //         yield return new WaitForEndOfFrame();
    //     }

    //     onComplete.Invoke();
    //     Destroy(obj.gameObject);
    // }

    IEnumerator MoveCoinTrans (Transform obj, Vector3 startPos, Transform endPos, Action onComplete)
    {
        Vector3 endPoint = cam.ScreenToWorldPoint(new Vector3 (endPos.position.x, endPos.position.y, cam.transform.position.z * -1));
        obj.position = startPos;

        while ((endPoint - obj.position).magnitude > 0.5f)
        {
            obj.Translate ((endPoint - obj.position).normalized * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();

            endPoint = cam.ScreenToWorldPoint(new Vector3(endPos.position.x, endPos.position.y, cam.transform.position.z * -1));
        }

        onComplete.Invoke();
        Destroy(obj.gameObject);
    }
}
