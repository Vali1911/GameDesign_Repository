using UnityEngine;

public class BulletDrag : MonoBehaviour
{
    // Referenz auf den übergeordneten Controller (BulletsInteraction)
    private BulletsInteraction controller;

    // Collider der EvidenceBag für die Trefferprüfung
    private Collider2D evidenceBag;

    // Collider des ComicPanels zur Begrenzung
    private Collider2D panelBounds;

    // Gibt an, ob diese Bullet bereits eingesammelt wurde
    private bool isCollected = false;

    // Startposition der Bullet (für Reset bei Fehlversuch)
    private Vector3 startPosition;

    // Gibt an, ob die Bullet aktuell gezogen wird
    private bool isDragging = false;

    // Initialisierung durch BulletsInteraction beim Start des Minigames
    public void Init(BulletsInteraction interaction, Collider2D bag, Collider2D bounds)
    {
        controller = interaction;
        evidenceBag = bag;
        panelBounds = bounds;

        // Ursprüngliche Position merken
        startPosition = transform.position;

        // Sicherheits-Reset
        isCollected = false;
        isDragging = false;
    }

    void Update()
    {
        // Maus gedrückt -> prüfen ob diese Bullet unter dem Cursor liegt
        if (Input.GetMouseButtonDown(0) && !isCollected)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

            foreach (var hit in hits)
            {
                if (hit.gameObject == gameObject)
                {
                    isDragging = true;
                    break;
                }
            }
        }

        // Während des Ziehens folgt die Bullet der Mausposition (begrenzt auf Panel)
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            transform.position = ClampToPanel(mousePos);
        }

        // Maus losgelassen -> Drop prüfen
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            // Prüfen, ob sich die Bullet über der EvidenceBag befindet
            if (evidenceBag != null && evidenceBag.OverlapPoint(transform.position))
            {
                CollectBullet();
            }
            else
            {
                // Falls nicht korrekt abgelegt -> zurück zur Startposition
                transform.position = startPosition;
            }
        }
    }

    // Begrenzt die Bullet innerhalb des ComicPanels
    private Vector3 ClampToPanel(Vector3 targetPosition)
    {
        if (panelBounds == null)
            return targetPosition;

        Bounds bounds = panelBounds.bounds;

        float clampedX = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);

        return new Vector3(clampedX, clampedY, 0f);
    }

    // Wird aufgerufen, wenn die Bullet korrekt in der EvidenceBag abgelegt wurde
    private void CollectBullet()
    {
        isCollected = true;

        // Bullet ausblenden
        gameObject.SetActive(false);

        // Controller über erfolgreichen Fund informieren
        controller.RegisterBulletCollected();
    }
}
