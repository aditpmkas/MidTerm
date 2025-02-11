using UnityEngine;

public class TrapGround : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false; // Supaya jatuh hanya sekali

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Awalnya tidak jatuh
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasFallen && collision.gameObject.CompareTag("Player"))
        {
            hasFallen = true;
            rb.bodyType = RigidbodyType2D.Dynamic; // Aktifkan gravitasi
            rb.velocity = new Vector2(0, -5f); // Jatuhkan dengan kecepatan
        }
    }
}
