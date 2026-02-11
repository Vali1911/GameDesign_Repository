using UnityEngine;
using UnityEngine.Events;

public class BodyOutlineInteraction : MonoBehaviour
{
    // Referenz auf das Outline-Sprite der Leiche
    public GameObject bodyOutline;

    // Icon, das die Interaktion auslöst
    public GameObject interactionIcon;

    // Referenz auf den Player (für Freeze-Logik)
    public PlayerMovement player;

    // Event für Progression (PanelBarrierController etc.)
    public UnityEvent OnInteractionCompleted;

    // Gibt an, ob sich der Spieler aktuell in der Interaktion befindet
    private bool isInteracting = false;

    // Verhindert mehrfaches Melden des Fortschritts
    private bool hasBeenInvestigated = false;

    // Startet die Interaktion (wird vom InteractiveObject aufgerufen)
    public void StartInteraction()
    {
        // Verhindert mehrfaches Starten
        if (isInteracting)
            return;

        isInteracting = true;

        // Player einfrieren (Bewegung + Animation stoppen)
        player.isInteracting = true;

        // Panel sichtbar machen
        gameObject.SetActive(true);

        // Outline anzeigen
        if (bodyOutline != null)
            bodyOutline.SetActive(true);
    }

    void Update()
    {
        // Eingabe nur während aktiver Interaktion erlauben
        if (!isInteracting)
            return;

        // Mit F wird das Outline ein- bzw. ausgeblendet
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleBodyOutline();
        }
    }

    // Schaltet das Outline an oder aus
    private void ToggleBodyOutline()
    {
        if (bodyOutline == null)
            return;

        bodyOutline.SetActive(!bodyOutline.activeSelf);

        // Wenn Outline ausgeblendet wird → Interaktion beenden
        if (!bodyOutline.activeSelf)
        {
            EndInteraction();
        }
    }

    // Beendet die Interaktion vollständig
    private void EndInteraction()
    {
        isInteracting = false;

        // Player wieder freigeben
        player.isInteracting = false;

        // Fortschritt nur beim ersten erfolgreichen Untersuchen melden
        if (!hasBeenInvestigated)
        {
            hasBeenInvestigated = true;
            OnInteractionCompleted?.Invoke();
        }

        // Icon dauerhaft deaktivieren
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        // Panel ausblenden
        gameObject.SetActive(false);
    }
}
