using UnityEngine;

public class BulletDrag : MonoBehaviour
{
    private BulletsInteraction controller;
    private Collider2D evidenceBag;
    private bool isCollected = false;

    private Vector3 startPosition;
    private bool isDragging = false;

    // Initialisierung durch BulletsInteraction
    public void Init(BulletsInteraction interaction, Collider2D bag)
    {
        controller = interaction;
        evidenceBag = bag;
        startPosition = transform.position;
        isCollected = false;
    }

    void OnMouseDown()
    {
        if (isCollected)
            return;

        isDragging = true;
    }

    void OnMouseUp()
    {
        if (!isDragging || isCollected)
            return;

        isDragging = false;

        // Prüfen, ob Bullet über EvidenceBag liegt
        if (evidenceBag.OverlapPoint(transform.position))
        {
            CollectBullet();
        }
        else
        {
            // Zurücksetzen, wenn daneben abgelegt
            transform.position = startPosition;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private void CollectBullet()
    {
        isCollected = true;

        // Bullet ausblenden oder fix in die Bag legen
        gameObject.SetActive(false);

        // Controller informieren
        controller.RegisterBulletCollected();
    }
}

