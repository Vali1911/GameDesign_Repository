using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    // Gibt an, ob sich der Player aktuell im Triggerbereich befindet
    private bool playerInside = false;

    // Referenz auf den Player (wird beim Betreten gesetzt)
    private PlayerMovement player;

    // Optional verknüpftes Objekt (z.B. Panel), das durch Interaktion aktiviert wird
    public GameObject linkedObject;

    // Event, das beim Drücken von F ausgelöst wird
    public UnityEvent OnInteraction;

    void Update()
    {
        // Interaktion nur erlauben, wenn der Player im Trigger steht
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            ToggleInteraction();
        }
    }

    // Löst das im Inspector hinterlegte Event aus
    private void ToggleInteraction()
    {
        OnInteraction?.Invoke();
    }

    // Wird aufgerufen, wenn der Player den Trigger betritt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;

            // Player-Referenz setzen
            player = collision.GetComponent<PlayerMovement>();
        }
    }

    // Wird aufgerufen, wenn der Player den Trigger verlässt
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;

            // Sicherheitshalber Interaktion beenden
            if (player != null)
                player.isInteracting = false;
        }
    }

    // Kann von anderen Scripts aufgerufen werden, um das Icon zu deaktivieren
    public void HideIcon()
    {
        gameObject.SetActive(false);
    }
}
