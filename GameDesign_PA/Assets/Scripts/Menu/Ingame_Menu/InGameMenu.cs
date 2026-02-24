using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isMenuOpen = false;

    [Header("Start Scene Name")]
    public string startSceneName = "StartScene";

    // Globale Info, ob Spiel pausiert ist
    public static bool IsGamePaused = false;

    void Start()
    {
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
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        isMenuOpen = true;

        IsGamePaused = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PauseAllSFX();
    }

    public void CloseMenu()
    {
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

        // Verhindert Ghost-Footsteps
        IsGamePaused = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.StopAllSFX();

        SceneManager.LoadScene(startSceneName);
    }
}