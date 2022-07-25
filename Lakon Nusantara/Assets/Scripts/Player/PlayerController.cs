using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public HealthBar healthBar;
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
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public BasicAttack basicAttack;
    public Quest quest;
    public QuestTimer questTimer;
    public Text quesetTimerText;
    public GameObject objectivesPanel;
    [Multiline]
    public Text ObjectivesText;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public ParticleSystem dust;
    public bool canMove = true;
    public bool defeated = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = health;
        healthBar.SetMaxHealth((int)health);
    }

    private void FixedUpdate() {
        if(canMove) {
            // If movement input is not 0, try to move
            if(movementInput != Vector2.zero){
                
                bool success = TryMove(movementInput);

                if(!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                
                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
                dust.Stop();
            }

            // Set direction of sprite to movement direction
            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }

        // Quest Objectives Status
        if(quest.isActive == true)
        {
            objectivesPanel.SetActive(true);
            ObjectivesText.text = quest.description + "\n" + quest.goal.currentAmount + " / " + quest.goal.requiredAmount;
        }
        else
        {
            objectivesPanel.SetActive(false);
        }

        // Quest Timer Status
        if(questTimer.TimerOn)
        {
            if(questTimer.TimeLeft > 0)
            {
                questTimer.TimeLeft -= Time.deltaTime;
                updateTimer(questTimer.TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                questTimer.TimeLeft = 0;
                questTimer.TimerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        quesetTimerText.text = string.Format("Tenggat waktu = " + "{0:00}:{1:00}", minutes, seconds);
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
        if (defeated != true) {
            dust.Play();
        }
        // bool success = TryMove(movementInput);
        // if(success) {
        //     dust.Play();
        // }
    }

    void OnFire() {
        animator.SetTrigger("basicAttack");
    }

    public void BasicAttack() {
        LockMovement();

        if(spriteRenderer.flipX == true){
            basicAttack.AttackLeft();
        } else {
            basicAttack.AttackRight();
        }
    }

    public void EndBasicAttack() {
        UnlockMovement();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        if(defeated != true)
        {
            canMove = true;
        }
    }

    public void Defeated() {
        defeated = true;
        LockMovement();
        animator.SetTrigger("Defeated");
        dust.Stop();
    }

    public void EnemyKilled()
    {
        if(quest.isActive)
        {
            quest.goal.EnemyKilled();
            if(quest.goal.IsReached())
            {
                quest.Complete();
            }
        }
    }
}
