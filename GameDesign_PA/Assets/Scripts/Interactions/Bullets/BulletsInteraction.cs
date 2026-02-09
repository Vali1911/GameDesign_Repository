using UnityEngine;
using UnityEngine.Events;

public class BulletsInteraction : MonoBehaviour
{
    // =========================
    // Referenzen
    // =========================

    // Interaktions-Icon (Ausrufezeichen)
    public GameObject interactionIcon;

    // Evidence Bag (Zielbereich)
    public Collider2D evidenceBag;

    // Alle Bullet-Objekte
    public BulletDrag[] bullets;

    // Player
    public PlayerMovement player;

    // Wird ausgelöst, wenn ALLE Bullets eingesammelt wurden
    public UnityEvent OnInteractionCompleted;

    // =========================
    // Interner Zustand
    // =========================

    private bool isInteracting = false;
    private int bulletsCollected = 0;

    // =========================
    // Start der Interaktion
    // (vom InteractiveObject aufgerufen)
    // =========================

    public void StartInteraction()
    {
        if (isInteracting)
            return;

        isInteracting = true;

        // Player einfrieren (wie AlarmClock / BodyOutline)
        player.isInteracting = true;

        // Panel anzeigen
        gameObject.SetActive(true);

        // Alle Bullets initialisieren
        foreach (var bullet in bullets)
        {
            bullet.Init(this, evidenceBag);
        }
    }

    // =========================
    // Wird von BulletDrag aufgerufen
    // =========================

    public void RegisterBulletCollected()
    {
        bulletsCollected++;

        if (bulletsCollected >= bullets.Length)
        {
            CompleteInteraction();
        }
    }

    // =========================
    // Interaktion erfolgreich beenden
    // =========================

    private void CompleteInteraction()
    {
        isInteracting = false;

        // Player freigeben
        player.isInteracting = false;

        // Icon deaktivieren (JETZT erst!)
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        // Fortschritt melden
        OnInteractionCompleted?.Invoke();

        // Panel ausblenden
        gameObject.SetActive(false);
    }
}
