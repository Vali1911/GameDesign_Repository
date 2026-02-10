using UnityEngine;
using UnityEngine.Events;

public class FingerPrintsInteraction : MonoBehaviour
{
    [Header("Core References")]
    public PlayerMovement player;
    public GameObject interactionIcon;

    [Header("Objects")]
    public GameObject powder;
    public GameObject tape;
    public GameObject evidenceBag;

    [Header("Fingerprint")]
    public SpriteRenderer fingerprintRenderer;
    public Sprite fingerprintNormal;
    public Sprite fingerprintRevealed;

    [Header("Attach Fingerprint To Tape")]
    public Vector3 fingerprintOffsetOnTape;
    public int fingerprintSortingOrderOnTape = 5;

    [Header("Events")]
    public UnityEvent OnInteractionCompleted;

    private bool isInteracting = false;
    private bool powderUsed = false;
    private bool tapeUsed = false;
    private bool completed = false;

    private GameObject draggedObject;
    private Vector3 dragOffset;

    // Wird vom InteractiveObject aufgerufen
    public void StartInteraction()
    {
        if (isInteracting || completed)
            return;

        isInteracting = true;
        player.isInteracting = true;

        gameObject.SetActive(true);

        // ✅ Zustand wiederherstellen
        if (tapeUsed)
        {
            evidenceBag.SetActive(true);
        }
        else
        {
            evidenceBag.SetActive(false);
        }
    }

    void Update()
    {
        if (!isInteracting)
            return;

        HandleDrag();

        // F → Interaction abbrechen
        if (Input.GetKeyDown(KeyCode.F))
        {
            EndInteraction(false);
        }
    }

    // =========================
    // DRAG & DROP
    // =========================

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null)
            {
                draggedObject = hit.gameObject;
                dragOffset = draggedObject.transform.position - (Vector3)mousePos;
            }
        }

        if (Input.GetMouseButton(0) && draggedObject != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            draggedObject.transform.position = mousePos + (Vector2)dragOffset;
        }

        if (Input.GetMouseButtonUp(0) && draggedObject != null)
        {
            CheckDrop(draggedObject);
            draggedObject = null;
        }
    }

    private void CheckDrop(GameObject obj)
    {
        // POWDER → Fingerprint
        if (obj == powder && !powderUsed && IsOverFingerprint(obj))
        {
            powderUsed = true;

            fingerprintRenderer.sprite = fingerprintRevealed;
            powder.SetActive(false);
            return;
        }

        // TAPE → Fingerprint (nur nach Powder)
        if (obj == tape && powderUsed && !tapeUsed && IsOverFingerprint(obj))
        {
            tapeUsed = true;

            AttachFingerprintToTape();
            evidenceBag.SetActive(true);
            return;
        }

        // TAPE (+ Fingerprint) → EvidenceBag
        if (obj == tape && tapeUsed && IsOverEvidenceBag(obj))
        {
            CompleteInteraction();
        }
    }

    // =========================
    // HELPERS
    // =========================

    private bool IsOverFingerprint(GameObject obj)
    {
        return Vector2.Distance(
            obj.transform.position,
            fingerprintRenderer.transform.position
        ) < 0.5f;
    }

    private bool IsOverEvidenceBag(GameObject obj)
    {
        return Vector2.Distance(
            obj.transform.position,
            evidenceBag.transform.position
        ) < 0.5f;
    }

    private void AttachFingerprintToTape()
    {
        fingerprintRenderer.transform.SetParent(tape.transform);
        fingerprintRenderer.transform.localPosition = fingerprintOffsetOnTape;
        fingerprintRenderer.sortingOrder = fingerprintSortingOrderOnTape;
    }

    // =========================
    // END / COMPLETE
    // =========================

    private void CompleteInteraction()
    {
        completed = true;

        OnInteractionCompleted?.Invoke();
        EndInteraction(true);
    }

    private void EndInteraction(bool success)
    {
        isInteracting = false;
        player.isInteracting = false;

        if (success && interactionIcon != null)
            interactionIcon.SetActive(false);

        gameObject.SetActive(false);
    }
}
