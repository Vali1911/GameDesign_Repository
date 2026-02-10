using UnityEngine;
using UnityEngine.Events;

public class BodyOutlineInteraction : MonoBehaviour
{
    public GameObject bodyOutline;          // NUR das Outline-Sprite
    public GameObject interactionIcon;      // Icon_BodyOutline
    public PlayerMovement player;

    public UnityEvent OnInteractionCompleted;

    private bool isInteracting = false;
    private bool hasBeenInvestigated = false;

    // Wird vom InteractiveObject aufgerufen
    public void StartInteraction()
    {
        if (isInteracting)
            return;

        isInteracting = true;

        // Player einfrieren (AlarmClock-Logik)
        player.isInteracting = true;

        // Panel aktivieren
        gameObject.SetActive(true);

        // Outline anzeigen
        if (bodyOutline != null)
            bodyOutline.SetActive(true);
    }

    void Update()
    {
        if (!isInteracting)
            return;

        // F toggelt Outline
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleBodyOutline();
        }
    }

    private void ToggleBodyOutline()
    {
        if (bodyOutline == null)
            return;

        bodyOutline.SetActive(!bodyOutline.activeSelf);

        // Wenn ausgeblendet → Interaktion beenden
        if (!bodyOutline.activeSelf)
        {
            EndInteraction();
        }
    }

    private void EndInteraction()
    {
        isInteracting = false;

        // Player freigeben
        player.isInteracting = false;

        // Progression NUR EINMAL melden
        if (!hasBeenInvestigated)
        {
            hasBeenInvestigated = true;
            OnInteractionCompleted?.Invoke();
        }

        // Icon ausblenden
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        // Panel ausblenden
        gameObject.SetActive(false);
    }
}
