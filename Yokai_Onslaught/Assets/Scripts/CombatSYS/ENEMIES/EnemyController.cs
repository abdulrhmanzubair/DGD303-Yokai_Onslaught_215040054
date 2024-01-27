using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public int maxHealth = 3;
    private int currentHealth;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isDead)
        {
            // Check if the enemy is on the ground
            bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            // Move the enemy
            Move();

            // Flip the enemy when changing direction
            if (!isGrounded)
            {
                Flip();
            }
        }
    }

    void Move()
    {
        // Move the enemy horizontally
        float horizontalMovement = isFacingRight ? moveSpeed : -moveSpeed;
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
    }

    void Flip()
    {
        // Flip the enemy's direction
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        // Perform death-related actions (e.g., play death animation, spawn particles, update score, etc.)
        Debug.Log("Enemy died!");

        // Optionally, you can destroy the GameObject or disable it
        // Destroy(gameObject);
        // gameObject.SetActive(false);
    }
}
