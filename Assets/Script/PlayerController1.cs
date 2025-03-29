using System;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 7f;
    bool isFacingRight = true;
    float jumpPower = 7f;
    bool isGrounded = true;

    Rigidbody2D rb;
    Animator animator;

    // Attack variables
    bool isAttacking = false; // Track if we're currently attacking
    float attackCooldown = 0f; // Time between attacks
    float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        // Attack logic: Detect attack input and trigger it only once per frame
        if (Input.GetButtonDown("Fire1") && !isAttacking) // "Fire1" is the attack button
        {
            isAttacking = true;
            animator.SetTrigger("Attack"); // Trigger the attack animation (only once per click)
            attackTimer = attackCooldown; // Reset cooldown timer
        }

        // Handle cooldown after attack
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            isAttacking = false; // Reset the attack flag after cooldown
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
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")){ // Ensure only ground makes player "grounded"
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")){ // Ensure only ground makes player "grounded"
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }
}   
