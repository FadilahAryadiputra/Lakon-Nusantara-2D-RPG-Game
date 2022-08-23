using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarksmanController : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

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
    public float health = 100;
    public float currentHealth;

    public bool canFire = true;
    public bool canMove = true;
    public bool isKnocked = false;
    public float isKnockedTime;
	public float knockTimer = 0f;
    public float moveSpeed;
    public float stoppingDistance = 6f;
    public float retreatDistance = 4f;
    [SerializeField]
    private float maxViewDistance = 8f;
    public Transform homePos;
    public PatrolAI patrolAI;
    public SetPositionToTarget setPositionToTarget;
    [SerializeField]
    private bool goHome = false;
    public bool followPlayer;
    public bool goPatrol;
    public float posDifference;
    GameObject respawner;

    public float startTimeBtwShots;
    [SerializeField]
    private float timeBtwShots;

    public GameObject bulletProjectile;
    public GameObject smokeFXPrefab;
    public Transform player;

    public GameObject HealthPotionPrefab;
    public GameObject coinPrefab;
    public float coinSpawnRange;
    public int minCoinSpawn = 1;
    public int maxCoinSpawn = 5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = health;
        enemyHealthBar.SetMaxHealth((int)health);
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            if(Vector2.Distance(transform.position, player.position) >= stoppingDistance && Vector2.Distance(transform.position, player.position) <= maxViewDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                animator.SetBool("isMoving", true);
                followPlayer = true;
                goHome = false;
                goPatrol = false;
                canFire = false;
            } else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance) {
                transform.position = this.transform.position;
                animator.SetBool("isMoving", false);
                followPlayer = false;
                goHome = false;
                goHome = false;
                goPatrol = false;
                canFire = true;
            } 
            else if(Vector2.Distance(transform.position, player.position) < retreatDistance) {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
                animator.SetBool("isMoving", true);
                followPlayer = false;
                goHome = false;
                goHome = false;
                goPatrol = false;
                canFire = true;
            } else if(Vector2.Distance(transform.position, player.position) >= maxViewDistance) {
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

            if(goHome)
            {
                if (transform.position.x > homePos.transform.position.x) {
                    spriteRenderer.flipX = true;
                } else if (transform.position.x < homePos.transform.position.x) {
                    spriteRenderer.flipX = false;
                }
            }

            if(!goHome)
            {
                if (transform.position.x > player.transform.position.x) {
                spriteRenderer.flipX = true;
                } else if (transform.position.x < player.transform.position.x) {
                    spriteRenderer.flipX = false;
                }
            }
        }

        if(patrolAI)
        {
            if(goPatrol)
            {
                patrolAI.isPatrolling = true;
                animator.SetBool("isMoving", true);
                posDifference = setPositionToTarget.targetPos.x - patrolAI.targetPos.x;
                if (posDifference > 0) {
                    spriteRenderer.flipX = true;
                } else if (posDifference < 0) {
                    spriteRenderer.flipX = false;
                } else {
                    animator.SetBool("isMoving", false);
                }
            } else {
                patrolAI.isPatrolling = false;
            }
        }

        if(canFire)
        {
            if(timeBtwShots <= 0)
            {
                animator.SetTrigger("Fire");
                // Fire();

                timeBtwShots = startTimeBtwShots;
            } else {
                timeBtwShots -= Time.deltaTime;
            }
        }

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
                UnlockAction();
            }
		}
    }
    
    public void GoHome()
    {
        if(canMove) {
            canFire = false;
            goHome = true;
            animator.SetFloat("moveX", (homePos.position.x - transform.position.x));
            animator.SetFloat("moveY", (homePos.position.y - transform.position.y));
            transform.position = Vector3.MoveTowards(transform.position, homePos.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, homePos.position) == 0)
            {
                animator.SetBool("isMoving", false);
                goHome = false;
            }
        }
    }

    void Fire()
    {
        // Spawn the projectile
        GameObject newProjectile = Instantiate(bulletProjectile, transform.position, Quaternion.identity);

        // Set projectile direction toward player
        Vector3 projectileToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(projectileToTarget.y, projectileToTarget.x) * Mathf.Rad2Deg;
        newProjectile.GetComponent<Rigidbody2D>().rotation = angle;
        projectileToTarget.Normalize();
    }

    public void LockAction()
    {
        canMove = false;
        canFire = false;
    }

    public void UnlockAction()
    {
        canMove = true;
        canFire = true;
    }

    public void InstantiateSmokeFX()
    {
        Instantiate(smokeFXPrefab, transform.position, Quaternion.identity);
    }

    void Defeated()
    {
        LockAction();
        Destroy(enemyHealthBar.gameObject);
        this.GetComponent<BoxCollider2D>().enabled = false;
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
        respawner.GetComponent<EnemyRespawn>().Death = true;
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    public void RemoveEnemyWithParent() {
        Destroy(transform.parent.gameObject);
    }
}
