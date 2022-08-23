using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float coinLifeTime = 10;
    public GameObject meter;
    public GameObject PopUpTextPrefab;
    public AudioClip coinSFX;
    Animator animator;
    public Rigidbody2D rb;
    private Transform target;
    [SerializeField]
    private float speed=0f;
    [SerializeField]
    private float maxRange=0f;
    [SerializeField]
    private float minRange=0f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        this.GetComponent<BoxCollider2D> ().isTrigger = false;
    }

    void Update()
    {
        // Coin Spawn isTrigger Delay
        StartCoroutine(CoinSpawnDelay());
        IEnumerator CoinSpawnDelay()
        {
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<BoxCollider2D> ().isTrigger = true;
        }

        // Follow Player if coin is close
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                FollowPlayer();
            }

        // Coin Life Time
        StartCoroutine(CoinLifeTime());
        IEnumerator CoinLifeTime() {
            yield return new WaitForSeconds(coinLifeTime);
            Destroy(gameObject);
        }
    }

    public void FollowPlayer()
    {
        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX", (target.position.x - transform.position.x));
        animator.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            // transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
            // // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            // // CoinScoreData.score += coinScore;
            
            AudioManager.instance.PlaySFX(coinSFX);
            // Destroy(gameObject);
            DestroyWithParent();
        }
    }

    public void DestroyWithParent() {
        Destroy(transform.parent.gameObject);
    }
}
