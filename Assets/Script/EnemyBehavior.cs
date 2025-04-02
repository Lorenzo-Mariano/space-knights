using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int maxHealth = 20;
    int currentHealth;

    public float moveSpeed = 1f;
    public float chaseSpeed = 2f;
    public float chaseRange = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Transform player;
    private Rigidbody2D rb;
    private bool hasDamagedPlayer = false; // Prevent multiple hits per collision

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();

        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }

        // Ensure the enemy sprite faces left
        transform.localScale = new Vector3(
            -Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);

        // Check for ground ahead
        if (!Physics2D.Raycast(groundCheck.position, Vector2.down, 1f, groundLayer))
        {
            StopMoving();
        }
    }

    void ChasePlayer()
    {
        rb.linearVelocity = new Vector2(-chaseSpeed, rb.linearVelocity.y);
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy health before dmg:" + currentHealth);
        currentHealth -= damage;
        Debug.Log("Enemy health after dmg:" + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died!");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDamagedPlayer)
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(1); // Damage the player once
                hasDamagedPlayer = true; // Prevent further damage

                Debug.Log("Enemy hit the player and got destroyed!"); // Log event

                Destroy(gameObject); // Destroy this enemy after hitting the player
            }
        }
    }
}
