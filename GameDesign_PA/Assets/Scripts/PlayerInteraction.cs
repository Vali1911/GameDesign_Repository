using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject drawerImage;      // UI-Popup Schublade
    public GameObject icon;             // Ausrufezeichen Icon
    public Collider2D barrierCollider;  // Barriere

    private bool playerInside = false;
    private bool drawerOpen = false;

    // Mappe anklicken
    public bool folderTaken = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            drawerOpen = !drawerOpen;
            drawerImage.SetActive(drawerOpen);

            // Wenn Schublade geschlossen UND Mappe wurde genommen
            if (!drawerOpen && folderTaken)
            {
                icon.SetActive(false);          // Icon ausblenden
                barrierCollider.enabled = false; // Spieler kann durch
            }
        }
    }

    // Ist der Player innerhalb des Icons?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            drawerOpen = false;
            drawerImage.SetActive(false);
        }
    }
}
