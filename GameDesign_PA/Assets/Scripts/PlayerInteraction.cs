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
        // Wenn man im Objekt steht UND F drückt
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            drawerOpen = !drawerOpen;
            drawerImage.SetActive(drawerOpen); // Object wird eingeblendet

            // Wenn Schublade geschlossen UND Mappe genommen wurde
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

    // Player im Object
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

    // Player nicht im Object
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