using UnityEngine;

public class BulletDrag : MonoBehaviour
{
    // Referenz auf den übergeordneten Controller (BulletsInteraction)
    private BulletsInteraction controller;

    // Collider der EvidenceBag für die Trefferprüfung
    private Collider2D evidenceBag;

    // Collider des Panels zur Bewegungsbegrenzung
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

        // Startposition nur beim ersten Initialisieren merken
        if (startPosition == Vector3.zero)
            startPosition = transform.position;
    }

    // Wird ausgelöst, wenn die Bullet angeklickt wird
    void OnMouseDown()
    {
        // Bereits eingesammelte Bullets dürfen nicht erneut bewegt werden
        if (isCollected)
            return;

        isDragging = true;
    }

    // Wird ausgelöst, wenn die Maustaste losgelassen wird
    void OnMouseUp()
    {
        // Nur reagieren, wenn tatsächlich gezogen wurde
        if (!isDragging || isCollected)
            return;

        isDragging = false;

        // Prüfen, ob sich die Bullet über der EvidenceBag befindet
        if (evidenceBag.OverlapPoint(transform.position))
        {
            CollectBullet();
        }
        else
        {
            // Falls nicht korrekt abgelegt -> zurück zur Startposition
            transform.position = startPosition;
        }
    }

    void Update()
    {
        // Während des Ziehens folgt die Bullet der Mausposition
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // Neue Position berechnen
            Vector3 newPos = mousePos;

            // Begrenzung auf die Panel-Grenzen
            if (panelBounds != null)
            {
                Bounds bounds = panelBounds.bounds;

                newPos.x = Mathf.Clamp(newPos.x, bounds.min.x, bounds.max.x);
                newPos.y = Mathf.Clamp(newPos.y, bounds.min.y, bounds.max.y);
            }

            transform.position = newPos;
        }
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

