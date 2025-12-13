using UnityEngine;

public class AlarmClockInteraction : MonoBehaviour
{
    public GameObject buttonSprite;       // Knopf auf Wecker
    public InteractiveObject iconObject;  // Referenz zum Icon-Prefab

    private bool isInteracting = false;

    void Start()
    {
        if (buttonSprite != null)
            buttonSprite.SetActive(true); // Knopf immer sichtbar
    }

    public void StartInteraction()
    {
        isInteracting = true;
    }

    public void EndInteraction()
    {
        isInteracting = false;

        // Icon verschwindet nach Interaktion
        if (iconObject != null)
            iconObject.HideIcon();
    }

    void Update()
    {
        if (isInteracting && Input.GetMouseButtonDown(0))
        {
            // Prüfen, ob der Knopf geklickt wird
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == buttonSprite)
            {
                Debug.Log("Wecker-Knopf gedrückt!");
                EndInteraction(); // Interaktion beenden
            }
        }
    }
}
