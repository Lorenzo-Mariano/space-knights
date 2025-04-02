using System;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI; // Import UI namespace

public class PlayerController : MonoBehaviour
{
    // physics and visual stuff
    Rigidbody2D rb;
    Animator animator;

    // movement stuff
    float horizontalInput;
    public float moveSpeed = 5f;
    bool isFacingRight = true;
    public float jumpPower = 5f;
    bool isGrounded = false;

    // Movement flags for buttons
    private bool moveLeft = false;
    private bool moveRight = false;

    // Attack variables
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    bool isAttacking = false;
    public float attackCooldown = 0.05f; // Cooldown for attack
    float attackTimer = 0f;

    // Health system
    public GameOverManager gameOverManager; // Reference to GameOverManager
    public int maxHealth = 3;
    private int currentHealth;
    public HealthBar healthBar;

    // points maybe
    public int score = 0;

    void Start()
    {
        enemyLayers = LayerMask.GetMask("Enemy");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Set player health to max at start
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Handle movement from buttons
        if (moveLeft)
            horizontalInput = -1f;
        else if (moveRight)
            horizontalInput = 1f;
        else
            horizontalInput = 0f;

        FlipSprite();

        // Attack logic
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime; // Reduce the timer every frame

            if (attackTimer <= 0f) // When timer expires, allow attacking again
            {
                isAttacking = false;
                attackTimer = 0f; // Ensure it doesn't go negative
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1); // Reduce health when touching an enemy
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        // animator.SetTrigger("Hurt");
        // Debug.Log("Player took damage! Current Health: " + currentHealth); // Log health update

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        moveSpeed = 0;

        Debug.Log("Player has died!"); // Log death event

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOverScreen();
        }
    }

    // Button functions
    public void MoveLeft(bool isPressed)
    {
        moveLeft = isPressed;
    }

    public void MoveRight(bool isPressed)
    {
        moveRight = isPressed;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            attackTimer = attackCooldown;
            animator.SetTrigger("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayers
            );

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
                if (enemyBehavior != null)
                {
                    enemyBehavior.TakeDamage(10);
                    Debug.Log("Enemy has been hit!");

                    if (enemyBehavior.currentHealth <= 0)
                    {
                        score += 10;
                        Debug.Log("Score added " + score);
                    }
                }
                else
                {
                    Debug.LogWarning("Hit an object that is not an enemy!");
                }
            }

            // attackTimer = attackCooldown;
        }
    }

    // debugging thing. Just so we can see the attack range of the player
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            Debug.Log("No attack point has been assigned! Or it seems at least.");
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
