using UnityEngine;
using UnityEngine.Events;

public class BodyOutlineInteraction : MonoBehaviour
{
    public GameObject bodyOutline;
    public GameObject interactionIcon;
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

        // Player einfrieren (identisch zur AlarmClock)
        player.isInteracting = true;

        // Panel aktivieren
        gameObject.SetActive(true);

        // Body Outline anzeigen
        if (bodyOutline != null)
            bodyOutline.SetActive(true);

        // Fortschritt nur EINMAL melden
        if (!hasBeenInvestigated)
        {
            hasBeenInvestigated = true;
            OnInteractionCompleted?.Invoke();
        }
    }

    void Update()
    {
        if (!isInteracting)
            return;

        // F toggelt Body Outline
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleBodyOutline();
        }
    }

    private void ToggleBodyOutline()
    {
        if (bodyOutline == null)
            return;

        bool active = bodyOutline.activeSelf;
        bodyOutline.SetActive(!active);

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

        // JETZT erst Icon deaktivieren (wie bei AlarmClock)
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        // Panel ausblenden
        gameObject.SetActive(false);
    }
}
