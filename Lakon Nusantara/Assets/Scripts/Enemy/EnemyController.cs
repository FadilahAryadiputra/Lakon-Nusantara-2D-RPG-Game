using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rb;
    private Transform target;
    public Transform homePos;
    [SerializeField]
    private float speed=0f;
    [SerializeField]
    private float maxRange=0f;
    [SerializeField]
    private float minRange=0f;

    public EnemyHealthBar enemyHealthBar;
    public float Health {
        set {
            health = value;

            if(health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }

    GameObject respawner;
    public float health = 100;
    public float currentHealth;
    public float damage = 20;
    public float thrustPower = 10f;
    public float knockTime = 0.3f;
    bool canMove = true;

    public Quest quest;
    public QuestGoal questGoal;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        currentHealth = health;
        enemyHealthBar.SetMaxHealth((int)health);
    }

    void Update()
    {
        if(canMove) {
            // If movement input is not 0, try to move
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                FollowPlayer();
            }
            else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                GoHome();
            }
        }
    }

    public void FollowPlayer()
    {
        if(canMove) {
            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", (target.position.x - transform.position.x));
            animator.SetFloat("moveY", (target.position.y - transform.position.y));
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    public void GoHome()
    {
        if(canMove) {
            animator.SetFloat("moveX", (homePos.position.x - transform.position.x));
            animator.SetFloat("moveY", (homePos.position.y - transform.position.y));
            transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, homePos.position) == 0)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            // Deal damage to the enemy
            PlayerController player = other.GetComponent<PlayerController>();
            CameraController camera = FindObjectOfType<CameraController>();

            if(player != null) {
                camera.ShakeCamera();
                player.Health -= damage;
                player.healthBar.SetHealth((int)player.Health);
                player.LockMovement();
                player.rb.isKinematic = false;
                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * thrustPower;
                player.rb.AddForce(difference, ForceMode2D.Impulse);
                player.rb.gravityScale = 0f;
                StartCoroutine(KnockCo());
            }
            IEnumerator KnockCo()
            {
                if(player != null) {
                    yield return new WaitForSeconds(knockTime);
                    player.rb.velocity = Vector2.zero;
                    player.rb.isKinematic = true;
                    player.UnlockMovement();
                }
            }
        }
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

    public void Defeated(){
        LockMovement();
        animator.SetTrigger("Defeated");
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnemyKilled();

        respawner = GameObject.Find(gameObject.name + (" spawn point"));
        respawner.GetComponent<EnemyRespawn>().Death = true;
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    public void RemoveEnemyWithParent() {
        Destroy(transform.parent.gameObject);
    }
}
