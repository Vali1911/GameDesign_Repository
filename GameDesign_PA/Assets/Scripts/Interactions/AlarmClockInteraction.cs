using UnityEngine;
using UnityEngine.Events;

public class AlarmClockInteraction : MonoBehaviour
{
    // Panel, das während der Interaktion angezeigt wird
    public GameObject AlarmClockPanel;

    // Klickbares Objekt (Wecker-Knopf)
    public GameObject Wecker;

    // Referenz auf den Player
    public PlayerMovement player;

    // Wird ausgelöst, wenn die Interaktion ERFOLGREICH abgeschlossen wurde
    public UnityEvent OnInteractionCompleted;

    private bool isInteracting = false;

    public void StartInteraction()
    {
        isInteracting = true;
        player.isInteracting = true;
        AlarmClockPanel.SetActive(true);
    }

    // Beendet die Interaktion
    private void EndInteraction(bool success)
    {
        isInteracting = false;
        player.isInteracting = false;

        AlarmClockPanel.SetActive(false);

        // NUR bei Erfolg das Event feuern
        if (success)
        {
            OnInteractionCompleted?.Invoke();
        }
    }

    void Update()
    {
        if (!isInteracting)
            return;

        // Interaktion abbrechen (F)
        if (Input.GetKeyDown(KeyCode.F))
        {
            EndInteraction(false);
            return;
        }

        // Wecker anklicken
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == Wecker)
            {
                Debug.Log("Wecker-Knopf gedrückt!");
                EndInteraction(true);
            }
        }
    }
}
