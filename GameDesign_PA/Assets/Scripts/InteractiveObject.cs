using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{

    private bool playerInside = false;

    public GameObject linkedObject;
    public UnityEvent OnInteraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        // Wenn man im Objekt steht UND F drückt
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            ToggleLinkedObject();
        }
    }

    private void ToggleLinkedObject()
    {
        bool newState = !linkedObject.activeSelf;
        linkedObject.SetActive(newState);
    }

    // Player im Object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    // Player nicht im Object
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
