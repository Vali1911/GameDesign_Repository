using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    private bool playerInside = false;
    private PlayerMovement player;

    public GameObject linkedObject; // Objekt, das angezeigt/aktiviert werden soll
    public UnityEvent OnInteraction;

    void Update()
    {
        // Player im Object und F gedrückt?
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            ToggleInteraction();
        }
    }

    // Object an/aus
    private void ToggleInteraction()
    {
        OnInteraction?.Invoke();
    }

    // Player im Object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            player = collision.GetComponent<PlayerMovement>();
        }
    }

    // Player nicht im Object
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;

            // Sicherheit: Interaction beenden beim Verlassen
            if (player != null)
                player.isInteracting = false;
        }
    }
    
    // Wird vom Wecker-Script aufgerufen, um das Icon zu verstecken
    public void HideIcon()
    {
        gameObject.SetActive(false);
    }
}
