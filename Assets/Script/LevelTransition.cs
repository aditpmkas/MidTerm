using UnityEngine;
using UnityEngine.SceneManagement; // Perlu untuk pindah scene

public class LevelTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Pastikan hanya Player yang bisa memicu
        {
            SceneManager.LoadScene("Level 2"); // Ganti dengan nama scene tujuan
        }
    }
}
