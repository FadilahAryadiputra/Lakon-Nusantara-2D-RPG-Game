using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    [Header("Health & Stamina Setting")]
    public HealthBar healthBar;
    public HealthBarSlowManager healthBarSlowManager;
    public StaminaBar staminaBar;
    public StaminaBarSlowManager staminaBarSlowManager;
    public float health = 100;
    public float currentHealth;
    public float stamina = 100;
    public float currentStamina;
    public int staminaRegen = 10;
    public int basicAttackStaminaUsage = 5;
    public int sprintStaminaUsage = 10;
    public int sprintStaminaDrain = 20;

    [Header("Action Setting")]
    public bool canMove = true;
    public bool moveSuccess;
    public bool defeated = false;
    public bool isKnocked = false;
    public float isKnockedTime;
	public float knockTimer = 0f;
    public bool isSprint = false;
    public float sprintSpeed = 2f;
    public float walkSpeed = 1f;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public ParticleSystem dustParticle;
    public ParticleSystem sprintParticle;
    public MovementJoystick movementJoystick;

    public Vector2 movementInput;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public BasicAttack basicAttack;

    [Header("Quest Setting")]
    public Quest quest;
    public QuestTimer questTimer;
    public QuestAchievement questAchievement;
    public bool allQuestCompleted;
    public TMP_Text questTimerTMP;
    public GameObject objectivesPanel;
    public TMP_Text ObjectivesTMP;
    
    [Header("Other Visual Setting")]
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer minimapIconSpriteRenderer;
    public Animator animator;
    public GameObject playerSpriteGO;
    public Vector3 playerSpriteOffset;

    public GameObject gameOverPanel;
    public GameObject gameOverDialogue;

    public CoinCollect coinCollect;
    public GameObject PopUpTextPrefab;
    public GameObject ItemPopUpTextPrefab;

    [Header("Audio Setting")]
    public AudioClip PlayerWalkSFX;
    public AudioClip BasicAttackSFX;
    public AudioClip CoinScoreSFX;
    public AudioClip objectivesSuccessSFX;
    public AudioClip PlayerDefeatedSFX;
    public AudioClip GameOverSFX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = playerSpriteGO.GetComponent<Animator>();
        spriteRenderer = playerSpriteGO.GetComponent<SpriteRenderer>();
        currentHealth = health;
        healthBar.SetMaxHealth((int)health);
        healthBarSlowManager.SetMaxHealth((int)health);
        currentStamina = stamina;
        staminaBar.SetMaxStamina((int)stamina);
        staminaBarSlowManager.SetMaxStamina((int)stamina);
        isSprint = false;
        moveSpeed = walkSpeed;
    }

    private void FixedUpdate() {
        if(currentHealth > health) {
            currentHealth = health;
        }
        if(currentHealth <= 0) {
            Defeated();
        }
        if(currentStamina < stamina && isSprint == false) {
            currentStamina += staminaRegen * Time.deltaTime;
        }

        // -- InputSystem --
        if(canMove) {
            // If movement input is not 0, try to move
            if(movementInput != Vector2.zero){
                
                moveSuccess = TryMove(movementInput);

                if(!moveSuccess) {
                    moveSuccess = TryMove(new Vector2(movementInput.x, 0));
                }

                if(!moveSuccess) {
                    moveSuccess = TryMove(new Vector2(0, movementInput.y));
                }
                
                animator.SetBool("isMoving", moveSuccess);
            } else {
                animator.SetBool("isMoving", false);
                movementInput = Vector2.zero;
            }

            // Set direction of sprite to movement direction
            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }

        if(minimapIconSpriteRenderer) {
            if(spriteRenderer.flipX == true)
                minimapIconSpriteRenderer.flipX = true;
            if(spriteRenderer.flipX == false)
                minimapIconSpriteRenderer.flipX = false;
        }

        if(currentStamina <= 0) {
            currentStamina = 0;
            isSprint = false;
        }

        if(isSprint) {
            moveSpeed = sprintSpeed;
            currentStamina -= sprintStaminaDrain * Time.deltaTime;
        } else if (!isSprint){
            moveSpeed = walkSpeed;
            sprintParticle.Stop();
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
            if(currentHealth > 0) {
                UnlockMovement();
            }
		}

        // Quest Objectives Status
        if(quest.isActive == true)
        {
            // Objectives Status
            ObjectivesTMP.text = quest.description + "\n" + quest.goal.currentAmount + " / " + quest.goal.requiredAmount;
            // Timer Status
            updateTimer(questTimer.TimeLeft);
            if(questTimer.TimerOn)
            {
                if(questTimer.TimeLeft > 0)
                {
                    questTimer.TimeLeft -= Time.deltaTime;
                }
                else
                {
                    questTimer.TimeLeft = 0;
                    questTimer.TimerOn = false;
                    questTimer.TimeIsUp = true;
                    Debug.Log("Time is UP!");
                }
            }
            if(quest.goal.currentAmount >= quest.goal.requiredAmount)
            {
                quest.completed = true;
            } else {
                quest.completed = false;
            }
        } else {
            ObjectivesTMP.text = "Belum ada misi";
            questTimerTMP.gameObject.SetActive(false);
        }

        if(questAchievement.questIndex[0] == true &&
            questAchievement.questIndex[1] == true)
        {
            allQuestCompleted = true;
        } else {
            allQuestCompleted = false;
        }
    }

    private void Update()
    {
        if(movementInput == Vector2.zero) {
            if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) ||
                Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow)) {
                dustParticle.Stop();
                AudioManager.instance.StopSFXLoop(PlayerWalkSFX);
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sprint();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitSprint();
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        questTimerTMP.text = string.Format("Tenggat waktu : " + "{0:00}:{1:00}", minutes, seconds);
    }

    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
        if (defeated != true && moveSuccess == true) {
            dustParticle.Play();
            AudioManager.instance.PlaySFXLoop(PlayerWalkSFX);
        }
    }

    void OnMoveJoystick(MovementJoystick movementJoystick) {

    }

    public void Sprint() {
        if (currentStamina > sprintStaminaUsage && movementInput != Vector2.zero)
        {
            isSprint = true;
            currentStamina = currentStamina - sprintStaminaUsage;
            sprintParticle.Play();
        }
    }

    public void ExitSprint() {
        if(isSprint == true)
        {
            isSprint = false;
        }
    }

    public void OnFire() {
        animator.SetTrigger("basicAttack");
    }

    public void Interact() {
        Debug.Log("Interacted!");
    }

    // public void BasicAttack() {
    //     LockMovement();
    //     currentStamina = currentStamina - basicAttackStaminaUsage;
    //     AudioManager.instance.PlaySFX(BasicAttackSFX);

    //     if(spriteRenderer.flipX == true){
    //         basicAttack.AttackLeft();
    //     } else {
    //         basicAttack.AttackRight();
    //     }
    // }

    // public void EndBasicAttack() {
    //     UnlockMovement();
    // }

    public void LockMovement() {
        canMove = false;
        isSprint = false;
    }

    public void UnlockMovement() {
        if(defeated != true)
        {
            canMove = true;
        }
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            animator.SetTrigger("Defeated");
            AudioManager.instance.PlaySFX(PlayerDefeatedSFX);
            AudioManager.instance.PlaySFX(GameOverSFX);
            if(gameOverPanel) {
                gameOverPanel.SetActive(true);
            }
            if(gameOverDialogue) {
                StartCoroutine(GameOverDialogue());

                IEnumerator GameOverDialogue() {
                    yield return new WaitForSeconds(3);
                    gameOverDialogue.SetActive(true);
                }
            }
        }
    }

    public void Defeated() {
        defeated = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        LockMovement();
        currentStamina = 0;
        dustParticle.Stop();
        AudioManager.instance.StopSFXLoop(PlayerWalkSFX);
    }

    public void EnemyKilled()
    {
        if(quest.isActive)
        {
            quest.goal.EnemyKilled();
            if(quest.goal.IsReached())
            {
                quest.completed = true;
            }
        }
    }

    public void GroupKilled()
    {
        if(quest.isActive)
        {
            quest.goal.GroupKilled();
            if(quest.goal.IsReached())
            {
                quest.completed = true;
            }
        }
    }

    public void ItemCollected()
    {
        if(quest.isActive)
        {
            quest.goal.ItemCollected();
            if(quest.goal.IsReached())
            {
                quest.completed = true;
            }
        }
    }

    void ShowCoinPopUpText()
    {
        var scoreGet = Instantiate(PopUpTextPrefab, transform.position, Quaternion.identity, transform);
        scoreGet.GetComponent<TMP_Text>().text = coinCollect.coinScore.ToString();
    }

    private void ShowItemPopUpText()
    {
        ItemController item = FindObjectOfType<ItemController>();
        var itemGet = Instantiate(ItemPopUpTextPrefab, transform.position, Quaternion.identity, transform);
        itemGet.GetComponent<TMP_Text>().text = item.itemName + " +1";
        // itemGet.GetComponent<TMP_Text>().text = coinCollect.coinScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            if(PopUpTextPrefab) {
                ShowCoinPopUpText();
            }

            coinCollect.StartCoinMove(other.transform.position, ()=>
            {
                CoinScoreData.score += coinCollect.coinScore;
                AudioManager.instance.PlaySFX(CoinScoreSFX);
            });
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Item"))
        {
            if(ItemPopUpTextPrefab) {
                ShowItemPopUpText();
            }
        }
    }
}
