using UnityEngine;
using UnityEngine.Tilemaps; // Jika menggunakan Tilemap

public class RevealObject : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer; // Untuk Tilemap
    private SpriteRenderer spriteRenderer; // Untuk objek biasa

    void Start()
    {
        // Coba ambil TilemapRenderer (jika ada)
        tilemapRenderer = GetComponent<TilemapRenderer>();
        if (tilemapRenderer != null)
        {
            tilemapRenderer.enabled = false; // Awalnya invisible
        }

        // Coba ambil SpriteRenderer (jika ada)
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Awalnya invisible
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Pastikan yang menyentuh adalah Player
        {
            if (tilemapRenderer != null)
            {
                tilemapRenderer.enabled = true; // Tampilkan Tilemap
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true; // Tampilkan Sprite biasa
            }
        }
    }
}
