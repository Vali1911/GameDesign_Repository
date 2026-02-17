using UnityEngine;
using UnityEngine.Events;

public class FingerPrintsInteraction : MonoBehaviour
{
    // Referenz auf den Player für Freeze-Logik
    public PlayerMovement player;

    // Icon, das die Interaktion startet
    public GameObject interactionIcon;

    // Interaktive Objekte im Minigame
    public GameObject powder;
    public GameObject tape;
    public GameObject evidenceBag;

    // Collider des ComicPanels zur Begrenzung
    public Collider2D panelBounds;

    // SpriteRenderer des Fingerabdrucks
    public SpriteRenderer fingerprintRenderer;

    // Ursprüngliches und freigelegtes Fingerprint-Sprite
    public Sprite fingerprintNormal;
    public Sprite fingerprintRevealed;

    // Offset-Position des Fingerprints relativ zum Tape
    public Vector3 fingerprintOffsetOnTape;

    // Sorting Order, damit der Fingerprint sichtbar über dem Tape liegt
    public int fingerprintSortingOrderOnTape = 5;

    // Event für Progression nach erfolgreichem Abschluss
    public UnityEvent OnInteractionCompleted;

    // Gibt an, ob das Panel aktuell aktiv ist
    private bool isInteracting = false;

    // Status, ob Powder bereits verwendet wurde
    private bool powderUsed = false;

    // Status, ob Tape bereits korrekt platziert wurde
    private bool tapeUsed = false;

    // Verhindert mehrfaches Abschließen
    private bool completed = false;

    // Aktuell gezogenes Objekt
    private GameObject draggedObject;

    // Offset zwischen Mausposition und Objekt beim Ziehen
    private Vector3 dragOffset;

    // Wird vom InteractiveObject aufgerufen
    public void StartInteraction()
    {
        if (isInteracting || completed)
            return;

        isInteracting = true;

        // Player einfrieren
        player.isInteracting = true;

        gameObject.SetActive(true);

        // Zustand beim erneuten Öffnen wiederherstellen
        if (tapeUsed)
            evidenceBag.SetActive(true);
        else
            evidenceBag.SetActive(false);
    }

    void Update()
    {
        if (!isInteracting)
            return;

        HandleDrag();

        // Mit F Interaktion abbrechen (kein Erfolg)
        if (Input.GetKeyDown(KeyCode.F))
        {
            EndInteraction(false);
        }
    }

    // Verarbeitet Maus-Input für Drag & Drop
    private void HandleDrag()
    {
        // Start des Ziehens
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

            foreach (var hit in hits)
            {
                if (hit.gameObject == powder || hit.gameObject == tape)
                {
                    draggedObject = hit.gameObject;
                    dragOffset = draggedObject.transform.position - (Vector3)mousePos;
                    break;
                }
            }
        }

        // Objekt folgt der Maus (begrenzt auf Panel)
        if (Input.GetMouseButton(0) && draggedObject != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = mousePos + (Vector2)dragOffset;

            draggedObject.transform.position = ClampToPanel(targetPosition);
        }

        // Ende des Ziehens -> Drop prüfen
        if (Input.GetMouseButtonUp(0) && draggedObject != null)
        {
            CheckDrop(draggedObject);
            draggedObject = null;
        }
    }

    // Begrenzt Objekte innerhalb des ComicPanels
    private Vector3 ClampToPanel(Vector3 targetPosition)
    {
        if (panelBounds == null)
            return targetPosition;

        Bounds bounds = panelBounds.bounds;

        float clampedX = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);

        return new Vector3(clampedX, clampedY, 0f);
    }

    // Prüft, ob ein Objekt korrekt abgelegt wurde
    private void CheckDrop(GameObject obj)
    {
        // Powder auf Fingerprint
        if (obj == powder && !powderUsed && IsOverFingerprint(obj))
        {
            powderUsed = true;

            fingerprintRenderer.sprite = fingerprintRevealed;
            powder.SetActive(false);
            return;
        }

        // Tape auf Fingerprint (nur nach Powder)
        if (obj == tape && powderUsed && !tapeUsed && IsOverFingerprint(obj))
        {
            tapeUsed = true;

            AttachFingerprintToTape();
            evidenceBag.SetActive(true);
            return;
        }

        // Tape mit Fingerprint in EvidenceBag
        if (obj == tape && tapeUsed && IsOverEvidenceBag(obj))
        {
            CompleteInteraction();
        }
    }

    // Prüft, ob ein Objekt nahe genug am Fingerprint ist
    private bool IsOverFingerprint(GameObject obj)
    {
        return Vector2.Distance(
            obj.transform.position,
            fingerprintRenderer.transform.position
        ) < 0.5f;
    }

    // Prüft, ob ein Objekt nahe genug an der EvidenceBag ist
    private bool IsOverEvidenceBag(GameObject obj)
    {
        return Vector2.Distance(
            obj.transform.position,
            evidenceBag.transform.position
        ) < 0.5f;
    }

    // Verbindet den Fingerprint visuell mit dem Tape
    private void AttachFingerprintToTape()
    {
        fingerprintRenderer.transform.SetParent(tape.transform);
        fingerprintRenderer.transform.localPosition = fingerprintOffsetOnTape;
        fingerprintRenderer.sortingOrder = fingerprintSortingOrderOnTape;
    }

    // Erfolgreicher Abschluss des Minigames
    private void CompleteInteraction()
    {
        completed = true;

        OnInteractionCompleted?.Invoke();
        EndInteraction(true);
    }

    // Beendet die Interaktion
    private void EndInteraction(bool success)
    {
        isInteracting = false;

        // Player wieder freigeben
        player.isInteracting = false;

        if (success && interactionIcon != null)
            interactionIcon.SetActive(false);

        gameObject.SetActive(false);
    }
}
