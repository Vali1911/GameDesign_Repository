using UnityEngine;
using UnityEngine.Events;

public class BodyOutlineInteraction : MonoBehaviour
{
    // =========================
    // Referenzen
    // =========================

    // Das Overlay / BackSprite, das während der Untersuchung sichtbar ist
    public GameObject backSprite;

    // Die Body Outline am Boden
    public GameObject bodyOutline;

    // Referenz auf den Player
    public PlayerMovement player;

    // Event, das beim ERSTEN erfolgreichen Untersuchen ausgelöst wird
    // (z. B. PanelBarrierController informieren)
    public UnityEvent OnInteractionCompleted;

    // =========================
    // Interner Zustand
    // =========================

    private bool playerInside = false;
    private bool isInteracting = false;
    private bool hasBeenInvestigated = false;

    // =========================
    // Update
    // =========================

    void Update()
    {
        // Player nicht im Trigger → nichts tun
        if (!playerInside)
            return;

        // F gedrückt
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Noch nicht in Interaktion → starten
            if (!isInteracting)
            {
                StartInteraction();
            }
            // Bereits in Interaktion → Body Outline toggeln / beenden
            else
            {
                ToggleBodyOutline();
            }
        }
    }

    // =========================
    // Interaktion starten
    // =========================

    private void StartInteraction()
    {
        isInteracting = true;
        player.isInteracting = true;

        // Player einfrieren
        player.enabled = false;

        // BackSprite anzeigen
        if (backSprite != null)
            backSprite.SetActive(true);

        // Body Outline anzeigen
        if (bodyOutline != null)
            bodyOutline.SetActive(true);

        // Erste erfolgreiche Untersuchung
        if (!hasBeenInvestigated)
        {
            hasBeenInvestigated = true;

            // Icon deaktivieren (nur einmal!)
            gameObject.SetActive(false);

            // Barrier / Progression informieren
            OnInteractionCompleted?.Invoke();
        }
    }

    // =========================
    // Body Outline an/aus
    // =========================

    private void ToggleBodyOutline()
    {
        if (bodyOutline != null)
        {
            bool isActive = bodyOutline.activeSelf;
            bodyOutline.SetActive(!isActive);
        }

        // Wenn Body Outline ausgeblendet wird → Interaktion beenden
        if (bodyOutline != null && !bodyOutline.activeSelf)
        {
            EndInteraction();
        }
    }

    // =========================
    // Interaktion beenden
    // =========================

    private void EndInteraction()
    {
        isInteracting = false;
        player.isInteracting = false;

        // Player freigeben
        player.enabled = true;

        // BackSprite ausblenden
        if (backSprite != null)
            backSprite.SetActive(false);
    }

    // =========================
    // Trigger
    // =========================

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

            // Sicherheit: Interaktion beenden
            if (isInteracting)
                EndInteraction();
        }
    }
}
