using UnityEngine;
using UnityEngine.Events;

public class BulletsInteraction : MonoBehaviour
{
    // Icon, das die Interaktion startet
    public GameObject interactionIcon;

    // Collider der EvidenceBag f¸r die Drop-Pr¸fung
    public Collider2D evidenceBag;

    // Collider des ComicPanels zur Bewegungsbegrenzung
    public Collider2D panelBounds;

    // Alle Bullet-Objekte dieses Minigames
    public BulletDrag[] bullets;

    // Referenz auf den Player f¸r Freeze-Logik
    public PlayerMovement player;

    // Event f¸r Progression (z.B. PanelBarrierController)
    public UnityEvent OnInteractionCompleted;

    // Gibt an, ob das Panel aktuell geˆffnet ist
    private bool isInteracting = false;

    // Verhindert mehrfaches Abschlieﬂen des Minigames
    private bool interactionCompleted = false;

    // Z‰hlt korrekt eingesammelte Bullets
    private int bulletsCollected = 0;

    // Startet oder schlieﬂt die Interaktion
    public void StartInteraction()
    {
        // Wenn bereits abgeschlossen -> keine erneute Interaktion
        if (interactionCompleted)
            return;

        // Wenn Panel bereits offen ist -> schlieﬂen
        if (isInteracting)
        {
            ClosePanel();
            return;
        }

        // Panel ˆffnen
        isInteracting = true;

        // Player einfrieren
        player.isInteracting = true;

        gameObject.SetActive(true);

        // Bullets initialisieren (Controller + EvidenceBag + PanelBounds ¸bergeben)
        foreach (var bullet in bullets)
        {
            if (bullet.gameObject.activeSelf)
                bullet.Init(this, evidenceBag, panelBounds);
        }
    }

    void Update()
    {
        // Eingabe nur w‰hrend aktiver Interaktion erlauben
        if (!isInteracting)
            return;

        // Mit F Panel ohne Erfolg schlieﬂen
        if (Input.GetKeyDown(KeyCode.F))
        {
            ClosePanel();
        }
    }

    // Wird von BulletDrag aufgerufen, wenn eine Bullet korrekt abgelegt wurde
    public void RegisterBulletCollected()
    {
        bulletsCollected++;

        // Wenn alle Bullets eingesammelt wurden -> Interaktion abschlieﬂen
        if (bulletsCollected >= bullets.Length)
        {
            CompleteInteraction();
        }
    }

    // Schlieﬂt das Panel ohne Erfolg
    private void ClosePanel()
    {
        isInteracting = false;

        // Player wieder beweglich machen
        player.isInteracting = false;

        gameObject.SetActive(false);
    }

    // Wird aufgerufen, wenn alle Bullets korrekt eingesammelt wurden
    private void CompleteInteraction()
    {
        interactionCompleted = true;
        isInteracting = false;

        // Player freigeben
        player.isInteracting = false;

        // Icon dauerhaft deaktivieren
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        // Progression melden
        OnInteractionCompleted?.Invoke();

        // Panel ausblenden
        gameObject.SetActive(false);
    }
}
