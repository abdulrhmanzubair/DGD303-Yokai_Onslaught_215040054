using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontal;
    public float jumpTime = 0.2f;
    public float speed = 8f;
    public float Dashspeed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private Animator anim;
    private float jumpTimeCounter;
    private float dashTimeCounter;

    private bool isWallSliding;
    public float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    //slide

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 70f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        dashTimeCounter = dashingTime;



    }

    private void Update()
    {
        
        horizontal = Input.GetAxisRaw("Horizontal");
        
        anim.SetFloat("RunSpeed", Mathf.Abs(horizontal));



        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
            Debug.Log("JUMP!");
            anim.SetBool("Jump", true);
           
        }
        if (Input.GetButton("Jump") && jumpTimeCounter > 0)
        {
            // Continuous jump while the jump button is held down
            Jump();
            jumpTimeCounter -= Time.deltaTime;
            anim.SetBool("Jump", true);
        }
        if (IsGrounded())
        {
            jumpTimeCounter = jumpTime;
            anim.SetBool("Jump", false);
        }

        if (Input.GetButtonDown("Jump") && IsWalled())
        {
            anim.SetBool("Wall_Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            Debug.Log("WALLJUMP!");
        }
        else
        {
            anim.SetBool("Wall_Jump", false);
        }

        if (isDashing)
        {
            dashTimeCounter = dashingTime;
            return;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            anim.SetBool("Jump", true);
        }
        
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            anim.SetBool("Dash", true);
            dashTimeCounter -= Time.deltaTime;
            StartCoroutine(Dash());
        }
        else{
            anim.SetBool("Dash", false);

        }

       

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        if (isDashing)
        {
            rb.velocity = new Vector2(horizontal * Dashspeed, rb.velocity.y);
            return;
        }

        
    }

   


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        
        
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
             anim.SetBool("Jump", false);
            anim.SetBool("Wall_Latch", true);
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            Debug.Log("WALL");
        }
        else
        {
            isWallSliding = false;
            anim.SetBool("Wall_Latch", false);
        }
        
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void Jump()
    {
        // Apply a vertical force to make the player jump
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        anim.SetBool("Jump", false);
        Debug.Log("DASH!");
    }

  
}