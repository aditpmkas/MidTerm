using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public static LifeSystem instance;

    [Header("UI Elements")]
    public GameObject deathPanel;
    public GameObject gameOverPanel;
    public Text lifeText;

    [Header("Nyawa Settings")]
    public int maxLives = 3;
    private int currentLives;

    private CheckpointSystem checkpointSystem;
    private Player player;
    private Vector2 startPosition;

    void Awake()
    {
        if (instance == null) instance = this;
    }


    void Start()
    {
        currentLives = maxLives;
        checkpointSystem = FindObjectOfType<CheckpointSystem>();
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            startPosition = player.transform.position; // Pastikan startPosition mengikuti posisi awal player
        }

        UpdateLifeUI();
    }


    public void TakeDamage()
    {
        currentLives--;
        UpdateLifeUI();

        if (currentLives > 0)
        {
            ShowDeathScreen();
            Invoke("RespawnPlayer", 1.5f);
        }
        else
        {
            GameOver();
        }
    }

    void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        Invoke("HideDeathScreen", 1.5f);
    }

    void HideDeathScreen()
    {
        deathPanel.SetActive(false);
    }
    void RespawnPlayer()
    {
        if (checkpointSystem != null && checkpointSystem.HasCheckpoint())
        {
            checkpointSystem.Respawn(); // Respawn ke checkpoint terakhir
        }
        else if (player != null)
        {
            player.RespawnAtStart(); // Jika belum ada checkpoint, respawn ke posisi awal
        }
    }



    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void UpdateLifeUI()
    {
        lifeText.text = "x " + currentLives;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}