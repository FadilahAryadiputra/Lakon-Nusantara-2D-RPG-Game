using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rb;
    private Transform target;
    public Transform homePos;
    public PatrolAI patrolAI;
    public bool followPlayer;
    public bool goHome;
    public bool goPatrol;
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
    public bool isKnocked = false;
    public float isKnockedTime;
	public float knockTimer = 0f;

    public GameObject HealthPotionPrefab;

    public CoinController CoinController;
    public GameObject coinPrefab;
    public float coinSpawnRange;
    public int minCoinSpawn = 1;
    public int maxCoinSpawn = 5;

    public Quest quest;
    public QuestGoal questGoal;
    public AudioClip HitPlayerSFX;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        currentHealth = health;
        enemyHealthBar.SetMaxHealth((int)health);
    }

    void FixedUpdate()
    {
        if(isKnocked)
		{
			knockTimer += Time.deltaTime;
		} else {
            knockTimer = 0f;
            isKnockedTime = 0f;
        }
		if(isKnocked && knockTimer >= isKnockedTime)
		{
            isKnocked = false;
			rb.velocity = Vector2.zero;
            if(health > 0) {
                UnlockMovement();
            }
		}
    }

    void Update()
    {
        if(canMove) {
            // If movement input is not 0, try to move
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                FollowPlayer();
                followPlayer = true;
                goHome = false;
                goPatrol = false;
            }
            else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                if(patrolAI)
                {
                    if(patrolAI.isInsidePatrolArea == false)
                    {
                        GoHome();
                        followPlayer = false;
                        goHome = true;
                        goPatrol = false;
                    }
                    else if(patrolAI.isInsidePatrolArea == true)
                    {
                        followPlayer = false;
                        goHome = false;
                        goPatrol = true;
                    }
                } else {
                    GoHome();
                    followPlayer = false;
                    goHome = true;
                    goPatrol = false;
                }
            }
            if(patrolAI)
            {
                if(goPatrol)
                {
                    patrolAI.isPatrolling = true;
                } else {
                    patrolAI.isPatrolling = false;
                }
            }
        }
    }

    public void FollowPlayer()
    {
        if(canMove) {
            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", (target.position.x - transform.position.x));
            animator.SetFloat("moveY", (target.position.y - transform.position.y));
            PlayerController player = FindObjectOfType<PlayerController>();
            Vector3 targetPos = target.transform.position + player.playerSpriteOffset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            CameraController camera = FindObjectOfType<CameraController>();

            if(player != null) {
                camera.ShakeCamera();
                player.currentHealth -= damage;
                AudioManager.instance.PlaySFX(HitPlayerSFX);
                // player.healthBar.SetHealth((int)player.currentHealth);
                player.LockMovement();
                player.rb.isKinematic = false;
                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * thrustPower;
                player.rb.AddForce(difference, ForceMode2D.Impulse);
                player.rb.gravityScale = 0f;

                // Start the timer to stop knockback
                player.isKnockedTime = knockTime;
				player.knockTimer = 0f;
				player.isKnocked = true;
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
        Destroy(enemyHealthBar.gameObject);
        animator.SetTrigger("Defeated");

        if(coinPrefab)
        {
            // Randomly pick the count of coin to spawn
            int count = Random.Range(this.minCoinSpawn, this.maxCoinSpawn );
            // Spawn the coin
            for (int i = 0; i < count; ++i) {
                // GameObject newCoin = Instantiate(coin, (Vector2)transform.position + Random.insideUnitCircle * coinSpawnRange, transform.rotation);
                GameObject newCoin = Instantiate(coinPrefab, (Vector2)transform.position, transform.rotation);

                newCoin.GetComponent<Rigidbody2D>().isKinematic = false;
                newCoin.GetComponent<Rigidbody2D>().gravityScale = 0f;
                var direction = Random.Range(-1000, 1000);
                var force = Random.Range(40,50);
                newCoin.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction,force));
                StartCoroutine(CoinDropDelay());

                IEnumerator CoinDropDelay()
                {
                    yield return new WaitForSeconds(0.2f);
                    newCoin.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    newCoin.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            }
        }

        if(HealthPotionPrefab)
        {
            // Randomly dropping HealthPotion
            if(Random.value > 0.80) //%20 percent chance (1 - 0.8 is 0.2)
            {
                GameObject newHealthPotion = Instantiate(HealthPotionPrefab, (Vector2)transform.position, transform.rotation);
            }
        }

        // Quest Progress
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnemyKilled();

        respawner = GameObject.Find(gameObject.name + (" spawn point"));
        if(respawner) {
            respawner.GetComponent<EnemyRespawn>().Death = true;
        } else {
            Debug.Log("There is no respawner for this enemy [" + gameObject.name + "]!");
        }
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    public void RemoveEnemyWithParent() {
        Destroy(transform.parent.gameObject);
    }
}
