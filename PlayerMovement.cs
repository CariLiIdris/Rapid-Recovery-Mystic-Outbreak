using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour

{
    public Player Data;

    public float horizontal;
    public float speed = 8f;
    [SerializeField] private int jumpPower = 15;
    [SerializeField] float fallMultiplier;
    public bool isFacingRight = true;
    Vector2 vecGravity;

    private Vector2 _moveInput;

    private Animator anim;

    [Header("Wall Jump System")]
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool KnockFromRight;

    private bool doubleJump;

    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public int weaponDamage = 25;

    //Coyote Time
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    //Jump Puffer Time
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Header("Dash Settings")]
    [SerializeField] float dashVelocity = 14f;
    [SerializeField] float dashTime = .5f;
    private Vector2 dashDirection;
    public bool isDashing;
    public bool canDash = false;

    private TrailRenderer trailRenderer;

    AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        anim = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.isActive == true)
        {
            anim.SetBool("isRunning", false);
            return;
        }

        //Jump
        horizontal = Input.GetAxisRaw("Horizontal");


        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            if (Data.CSLevel > 1)
            {
            canDash = true;
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            audioManager.PlaySFX(audioManager.jump);
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            doubleJump = false;
            anim.SetTrigger("takeoff");
            jumpBufferCounter = 0f;
        }

        if (doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            doubleJump = false;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        //Anims
        if (horizontal == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (IsGrounded())
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isAttacking", true);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashDirection = new Vector2(horizontal, Input.GetAxisRaw("Vertical"));
            if (dashDirection == Vector2.zero)
            {
                dashDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        }

        anim.SetBool("isDashing", isDashing);

        if (isDashing)
        {
            rb.velocity = dashDirection.normalized * dashVelocity;
            return;
        }

    }

    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameObject in enemy)
        {
            Debug.Log("Hit enemy");
            enemyGameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    public void endAttack()
    {
        anim.SetBool("isAttacking", false);
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
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallSliding = false;
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

            Invoke(nameof(StopWallJumping), wallJumpingDirection);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);

        }
    }
}
