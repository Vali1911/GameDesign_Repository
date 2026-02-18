using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isMenuOpen = false;

    [Header("Start Scene Name")]
    public string startSceneName = "StartScene";

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
    }

    public void CloseMenu()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        isMenuOpen = false;
    }

    // NEU: Zurück zur StartScene
    public void ReturnToStart()
    {
        Time.timeScale = 1f; // Sicherheit: Zeit wieder normal setzen
        SceneManager.LoadScene(startSceneName);
    }
}
