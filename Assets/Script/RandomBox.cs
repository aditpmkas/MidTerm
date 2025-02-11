using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public GameObject itemPrefab; // Prefab item yang muncul (Ikan)
    public Transform spawnPoint; // Tempat item muncul (di atas box)
    public float popUpHeight = 0.5f; // Seberapa tinggi item muncul sebelum menghilang
    public float popUpDuration = 0.5f; // Waktu item muncul sebelum menghilang

    private bool isUsed = false; // Supaya box hanya bisa dipukul sekali
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
        if (other.CompareTag("Player") && !isUsed) // Jika Player menyentuh dari bawah
        {
            isUsed = true;
            if (triggerCollider != null) triggerCollider.enabled = false; // Matikan trigger, tetap solid
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        GameObject item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
        StartCoroutine(MoveUpAndDisappear(item));
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

        Destroy(item); // Hapus item setelah muncul
        ScoreManager.instance.AddScore(1); // Tambah skor
    }
}