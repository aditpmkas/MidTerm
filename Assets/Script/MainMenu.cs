using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelPanel; // Assign LevelPanel di Inspector

    public void PlayGame()
    {
        levelPanel.SetActive(true); // Menampilkan panel pilihan level
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1"); // Pastikan nama scene sesuai
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void QuitGame()
    {
        Application.Quit(); // Keluar dari game
        Debug.Log("Game Closed!"); // Debug jika di editor
    }
}
