using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float acceleration = 15f;
    public float deceleration = 20f;

    [Header("Jump Settings")]
    public float jumpForce = 12f;
    public float maxJumpHoldTime = 0.2f;

    [Header("Gravity Settings")]
    public float gravityMultiplier = 2.5f;
    public float fallMultiplier = 8f;
    public float fastFallBoost = 2f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Audio Settings")]
    public AudioSource jumpSound; // Tambahkan AudioSource untuk jump sound

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private float jumpTimeCounter;
    private bool isJumping;
    private float moveInput;
    private float currentSpeed = 0f;
    private Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityMultiplier;
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        ApplyCustomGravity();
        UpdateAnimation();
    }

    void HandleMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput * moveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x),
                transform.localScale.y, transform.localScale.z);
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpHoldTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");

            // **Memainkan suara lompat langsung tanpa delay**
            if (jumpSound != null)
            {
                jumpSound.PlayOneShot(jumpSound.clip);
            }
        }

        if (Input.GetKey(KeyCode.W) && isJumping && jumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }


    void ApplyCustomGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
            rb.velocity += Vector2.down * fastFallBoost * Time.deltaTime;
        }
        else
        {
            rb.gravityScale = gravityMultiplier;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void UpdateAnimation()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            anim.Play("run 1_Clip");
        }
        else
        {
            anim.Play("front_Clip");
        }

        anim.SetBool("isJumping", !isGrounded);
    }

    public void RespawnAtStart()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
    }
}
