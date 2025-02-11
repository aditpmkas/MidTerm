using UnityEngine;

public class TrapBox : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab musuh yang akan muncul
    public Transform spawnPoint; // Tempat musuh muncul (di atas box)
    public float popUpHeight = 0.5f; // Seberapa tinggi musuh muncul
    public float popUpDuration = 0.5f; // Waktu musuh muncul sebelum bergerak

    private bool isTriggered = false; // Supaya box hanya bisa dipukul sekali
    private Collider2D triggerCollider; // Collider yang akan dimatikan
    private Collider2D solidCollider; // Collider yang tetap aktif

    void Start()
    {
        // Ambil collider dari objek ini
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (col.isTrigger) triggerCollider = col; // Simpan trigger collider
            else solidCollider = col; // Simpan collider solid
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered) // Jika Player menyentuh dari bawah
        {
            isTriggered = true;
            if (triggerCollider != null) triggerCollider.enabled = false; // Matikan trigger, tetap solid
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        StartCoroutine(MoveUpAndActivate(enemy));
    }

    System.Collections.IEnumerator MoveUpAndActivate(GameObject enemy)
    {
        Vector3 startPos = enemy.transform.position;
        Vector3 endPos = startPos + Vector3.up * popUpHeight;
        float elapsedTime = 0f;

        // Efek musuh naik ke atas sebelum bergerak
        while (elapsedTime < popUpDuration)
        {
            enemy.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / popUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Aktifkan AI Musuh jika ada script Enemy di dalamnya
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.enabled = true; // Aktifkan pergerakan musuh
        }
    }
}
