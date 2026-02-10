using UnityEngine;
using UnityEngine.Events;

public class BulletsInteraction : MonoBehaviour
{
    // =========================
    // Referenzen
    // =========================

    public GameObject interactionIcon;      // Icon_Bullets
    public Collider2D evidenceBag;           // Evidence Bag Collider
    public BulletDrag[] bullets;             // Alle Bullet-Objekte
    public PlayerMovement player;

    public UnityEvent OnInteractionCompleted;

    // =========================
    // Interner Zustand
    // =========================

    private bool isInteracting = false;
    private bool interactionCompleted = false;
    private int bulletsCollected = 0;

    // =========================
    // Start der Interaktion
    // =========================

    public void StartInteraction()
    {
        // Wenn bereits abgeschlossen -> nichts mehr tun
        if (interactionCompleted)
            return;

        // Wenn Panel offen ist -> F schlieﬂt es
        if (isInteracting)
        {
            ClosePanel();
            return;
        }

        // Panel ˆffnen
        isInteracting = true;
        player.isInteracting = true;
        gameObject.SetActive(true);

        // Bullets initialisieren (nur die noch nicht eingesammelten)
        foreach (var bullet in bullets)
        {
            bullet.Init(this, evidenceBag);
        }
    }

    void Update()
    {
        if (!isInteracting)
            return;

        // F schlieﬂt das Panel (ohne Erfolg)
        if (Input.GetKeyDown(KeyCode.F))
        {
            ClosePanel();
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
    // Panel schlieﬂen (ohne Erfolg)
    // =========================

    private void ClosePanel()
    {
        isInteracting = false;
        player.isInteracting = false;
        gameObject.SetActive(false);
    }

    // =========================
    // Erfolgreich abgeschlossen
    // =========================

    private void CompleteInteraction()
    {
        interactionCompleted = true;
        isInteracting = false;

        // Player freigeben
        player.isInteracting = false;

        // Icon deaktivieren
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        // Progression melden
        OnInteractionCompleted?.Invoke();

        // Panel ausblenden
        gameObject.SetActive(false);
    }
}
