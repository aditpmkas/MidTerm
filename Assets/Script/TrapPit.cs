using UnityEngine;

public class TrapPit : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab musuh yang akan muncul
    public Transform spawnPoint; // Tempat musuh muncul
    public float moveSpeed = 8f; // Kecepatan musuh naik
    private bool isTriggered = false; // Supaya tidak berulang

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered) // Jika Player menyentuh trigger
        {
            isTriggered = true;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed); // Langsung atur kecepatan naik

        Destroy(enemy, 4f); // Hancurkan musuh setelah 4 detik
    }
}
