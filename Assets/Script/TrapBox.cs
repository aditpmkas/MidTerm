using UnityEngine;

public class TrapBox : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float popUpHeight = 0.5f;
    public float popUpDuration = 0.5f;
    public AudioClip trapSound; // Tambahkan Audio Clip untuk suara jebakan
    private AudioSource audioSource; // Tambahkan Audio Source

    private bool isTriggered = false;
    private Collider2D triggerCollider;
    private Collider2D solidCollider;

    void Start()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (col.isTrigger) triggerCollider = col;
            else solidCollider = col;
        }

        audioSource = GetComponent<AudioSource>(); // Ambil AudioSource dari GameObject
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            if (triggerCollider != null) triggerCollider.enabled = false;
            SpawnEnemy();

            if (trapSound != null && audioSource != null)
            {
                Debug.Log("Playing trap sound!"); //  Debug cek suara dipanggil
                audioSource.PlayOneShot(trapSound);
            }
            else
            {
                Debug.LogError("AudioSource atau AudioClip tidak ditemukan!");
            }
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

        while (elapsedTime < popUpDuration)
        {
            enemy.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / popUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.enabled = true;
        }
    }
}
