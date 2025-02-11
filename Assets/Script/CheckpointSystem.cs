using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private Vector2 checkpointPosition;
    private bool hasCheckpoint = false;

    void Start()
    {
        checkpointPosition = Vector2.zero; // Default tidak ada checkpoint
    }

    public void SetCheckpoint(Vector2 position)
    {
        checkpointPosition = position;
        hasCheckpoint = true;
        Debug.Log("Checkpoint updated: " + position);
    }

    public Vector2 GetCheckpoint()
    {
        return checkpointPosition;
    }

    public bool HasCheckpoint()
    {
        return hasCheckpoint;
    }

    public void Respawn()
    {
        if (hasCheckpoint)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.transform.position = checkpointPosition; // Pindahkan player ke checkpoint
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset kecepatan
            }
        }
    }

    // Fungsi ini akan dipanggil oleh bendera sebagai checkpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Jika player menyentuh checkpoint
        {
            SetCheckpoint(transform.position);
        }
    }
}
