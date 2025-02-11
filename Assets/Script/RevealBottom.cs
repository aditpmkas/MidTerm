using UnityEngine;
using UnityEngine.Tilemaps;

public class RevealBottom : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Collider2D solidCollider;
    private Collider2D triggerCollider;

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        Collider2D[] colliders = GetComponents<Collider2D>();

        // Ambil collider yang trigger dan yang solid
        foreach (var col in colliders)
        {
            if (col.isTrigger)
                triggerCollider = col; // Collider yang tetap aktif (Trigger)
            else
                solidCollider = col; // Collider yang nonaktif dulu (Solid)
        }

        tilemapRenderer.enabled = false; // Objek awalnya tidak terlihat
        solidCollider.enabled = false;  // Collider solid awalnya mati
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            // Cek apakah Player menyentuh dari bawah (kecepatan Y positif berarti naik)
            if (rb.velocity.y > 0)
            {
                tilemapRenderer.enabled = true; // Munculkan objek
                solidCollider.enabled = true;  // Aktifkan collider agar bisa dipijak
            }
        }
    }
}