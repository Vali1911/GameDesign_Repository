using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool playerInside = false;
    private bool drawerOpen = false;

    public Collider2D barrierCollider;  // Barriere

    // UI
    public GameObject drawerImage;      // UI-Popup Schublade
    public GameObject icon;             // Ausrufezeichen Icon
    public BlinkArrow arrow;            // Pfeil Animation

    // Folder
    public bool folderTaken = false;

    void Update()
    {
        HandleDrawerInteraction();
    }

    private void HandleDrawerInteraction()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            drawerOpen = !drawerOpen;
            drawerImage.SetActive(drawerOpen);

            // Wenn Schublade geschlossen UND Mappe wurde genommen
            if (!drawerOpen && folderTaken)
            {
                icon.SetActive(false);            // Icon ausblenden
                barrierCollider.enabled = false;  // Spieler kann durch

                // Pfeil anzeigen
                if (arrow != null)
                    arrow.ShowArrow();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }

        // Spieler erreicht das nächste Panel
        if (collision.CompareTag("NextPanel") && arrow != null)
        {
            arrow.HideArrow();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            drawerOpen = false;

            if (drawerImage != null)
            {
                drawerImage.SetActive(false);
            }
        }
    }
}