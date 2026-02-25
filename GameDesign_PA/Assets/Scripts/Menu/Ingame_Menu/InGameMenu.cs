using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isMenuOpen = false;

    [Header("Start Scene Name")]
    public string startSceneName = "StartScene";

    public static bool IsGamePaused = false;

    void Start()
    {
        // 🔥 WICHTIG: Reset bei Szenenstart
        Time.timeScale = 1f;
        IsGamePaused = false;

        if (menuUI != null)
            menuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
                CloseMenu();
            else
                OpenMenu();
        }
    }

    public void OpenMenu()
    {
        if (menuUI != null)
            menuUI.SetActive(true);

        Time.timeScale = 0f;
        isMenuOpen = true;
        IsGamePaused = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PauseAllSFX();
    }

    public void CloseMenu()
    {
        if (menuUI != null)
            menuUI.SetActive(false);

        Time.timeScale = 1f;
        isMenuOpen = false;
        IsGamePaused = false;

        if (AudioManager.Instance != null)
            AudioManager.Instance.ResumeAllSFX();
    }

    public void ReturnToStart()
    {
        Time.timeScale = 1f;

        // Stoppt alle laufenden Sounds
        if (AudioManager.Instance != null)
            AudioManager.Instance.StopAllSFX();

        // ❗ NICHT mehr auf true setzen
        IsGamePaused = false;

        SceneManager.LoadScene(startSceneName);
    }
}