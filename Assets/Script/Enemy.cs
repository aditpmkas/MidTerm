using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform frontCheck;
    public bool startMovingRight = false;

    private Rigidbody2D rb;
    private bool movingRight;
    private bool canDamage = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movingRight = startMovingRight;

        if (!movingRight)
        {
            Flip();
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika menabrak player, serang
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            StartCoroutine(DamagePlayer(collision.gameObject.GetComponent<Player>()));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Jika menabrak objek dengan tag "Items", berbalik arah
        if (other.CompareTag("Items"))
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    System.Collections.IEnumerator DamagePlayer(Player player)
    {
        canDamage = false;
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;

        if (player != null)
        {
            LifeSystem.instance.TakeDamage();
        }

        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}
