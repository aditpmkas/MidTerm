using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform spawnPoint;
    public float popUpHeight = 0.5f;
    public float popUpDuration = 0.5f;
    public AudioClip collectSound; // Tambahkan Audio Clip untuk suara item
    private AudioSource audioSource; // Tambahkan Audio Source

    private bool isUsed = false;
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
        if (other.CompareTag("Player") && !isUsed)
        {
            isUsed = true;
            if (triggerCollider != null) triggerCollider.enabled = false;
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        GameObject item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
        StartCoroutine(MoveUpAndDisappear(item));

        if (collectSound != null && audioSource != null)
        {
            Debug.Log("Playing collect sound!"); // 🛠 Debug cek suara dipanggil
            audioSource.PlayOneShot(collectSound);
        }
        else
        {
            Debug.LogError("AudioSource atau AudioClip tidak ditemukan!");
        }
    }



    System.Collections.IEnumerator MoveUpAndDisappear(GameObject item)
    {
        Vector3 startPos = item.transform.position;
        Vector3 endPos = startPos + Vector3.up * popUpHeight;
        float elapsedTime = 0f;

        while (elapsedTime < popUpDuration)
        {
            item.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / popUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(item);
        ScoreManager.instance.AddScore(1);
    }
}
