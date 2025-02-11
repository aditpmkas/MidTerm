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

    private Rigidbody2D rb;
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
        startPosition = transform.position; // Simpan posisi awal saat game dimulai
    }


    void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        ApplyCustomGravity();
    }

    void HandleMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput * moveSpeed,
            (moveInput != 0 ? acceleration : deceleration) * Time.deltaTime);

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(-moveInput) * Mathf.Abs(transform.localScale.x),
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
    public void RespawnAtStart()
    {
        transform.position = startPosition; // Kembalikan player ke posisi awal
        rb.velocity = Vector2.zero; // Reset kecepatan agar tidak ada momentum saat respawn
    }



}
