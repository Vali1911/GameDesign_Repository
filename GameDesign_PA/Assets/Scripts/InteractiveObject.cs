using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    private bool playerInside = false;
    private PlayerMovement player; // Referenz zum PlayerMovement-Script

    public GameObject linkedObject;        // Objekt, das angezeigt/aktiviert werden soll
    public SpriteRenderer backViewSprite;  // SpriteRenderer für BackView
    public Sprite backSprite;              // Sprite, das angezeigt werden soll
    public UnityEvent OnInteraction;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            StartInteraction();
        }

        if (playerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            EndInteraction();
        }
    }

    private void StartInteraction()
    {
        // Player Bewegung deaktivieren
        if (player != null)
            player.enabled = false;

        // BackView Sprite anzeigen
        if (backViewSprite != null && backSprite != null)
        {
            backViewSprite.sprite = backSprite;
            backViewSprite.gameObject.SetActive(true);
        }

        // Optional: Event auslösen
        OnInteraction?.Invoke();

        // linkedObject aktivieren
        if (linkedObject != null)
            linkedObject.SetActive(true);
    }

    private void EndInteraction()
    {
        // Player Bewegung wieder aktivieren
        if (player != null)
            player.enabled = true;

        // BackView Sprite ausblenden
        if (backViewSprite != null)
            backViewSprite.gameObject.SetActive(false);

        // linkedObject deaktivieren
        if (linkedObject != null)
            linkedObject.SetActive(false);

        playerInside = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            player = collision.GetComponent<PlayerMovement>(); // PlayerMovement speichern
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndInteraction(); // Interaktion beenden, wenn Spieler rausgeht
        }
    }
}
