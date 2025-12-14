using UnityEngine;

public class AlarmClockInteraction : MonoBehaviour
{
    public GameObject AlarmClockPanel; // Panel das eingeblendet wird
    public GameObject Wecker;       // Objekt zum drücken
    public InteractiveObject iconObject;  // Referenz zum Icon-Prefab
    public PlayerMovement player;

    private bool isInteracting = false;

    public void StartInteraction()
    {
        isInteracting = true;
        player.isInteracting = true;
        AlarmClockPanel.SetActive(true);
    }

    public void EndInteraction(bool success)
    {
        isInteracting = false;
        player.isInteracting = false;

        AlarmClockPanel.SetActive(false);

        if (success && iconObject != null)
        {
            iconObject.HideIcon();
        }
    }

    void Update()
    {
        if (!isInteracting) return;

        // Fenster mit F schließen
        if (Input.GetKeyDown(KeyCode.F))
        {
            EndInteraction(false); // Interaktion nicht erledigt
            return;
        }

        // Knopf klicken
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == Wecker)
            {
                Debug.Log("Wecker-Knopf gedrückt!");
                EndInteraction(true); // erledigt
            }
        }
    }
}
