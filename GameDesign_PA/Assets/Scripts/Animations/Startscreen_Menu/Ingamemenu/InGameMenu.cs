using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isMenuOpen = false;

    void Start()
    {
        menuUI.SetActive(false); // Menü ist am Anfang unsichtbar
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
        Time.timeScale = 0f; // Spiel pausieren
        isMenuOpen = true;
    }

    public void CloseMenu()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f; // Spiel fortsetzen
        isMenuOpen = false;
    }
}