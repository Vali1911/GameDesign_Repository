using UnityEngine;
using UnityEngine.Events;

public class AlarmClockInteraction : MonoBehaviour
{
    // Panel, das während der Interaktion eingeblendet wird
    public GameObject AlarmClockPanel;

    // Klickbares Objekt innerhalb des Panels (Wecker-Knopf)
    public GameObject Wecker;

    // Referenz auf den Player für Freeze-Logik
    public PlayerMovement player;

    [Header("Audio")]
    public AudioClip alarmClip;

    // Event, das bei erfolgreichem Abschluss ausgelöst wird (Progression)
    public UnityEvent OnInteractionCompleted;

    private bool isInteracting = false;

    public void StartInteraction()
    {
        isInteracting = true;

        player.isInteracting = true;
        AlarmClockPanel.SetActive(true);

        // 🔊 Alarm starten (Loop)
        if (AudioManager.Instance != null && alarmClip != null)
        {
            AudioManager.Instance.PlayLoopingSFX(alarmClip);
        }
    }

    private void EndInteraction(bool success)
    {
        isInteracting = false;

        player.isInteracting = false;
        AlarmClockPanel.SetActive(false);

        // 🔇 Alarm stoppen
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopLoopingSFX();
        }

        if (success)
        {
            OnInteractionCompleted?.Invoke();
        }
    }

    void Update()
    {
        if (!isInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            EndInteraction(false);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == Wecker)
            {
                EndInteraction(true);
            }
        }
    }
}