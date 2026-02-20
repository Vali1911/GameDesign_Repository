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

    // NEU: AudioSource für den Alarm
    public AudioSource alarmAudio;

    // Event, das bei erfolgreichem Abschluss ausgelöst wird (Progression)
    public UnityEvent OnInteractionCompleted;

    // Gibt an, ob die Interaktion aktuell läuft
    private bool isInteracting = false;

    // Startet die Interaktion (wird vom InteractiveObject aufgerufen)
    public void StartInteraction()
    {
        isInteracting = true;

        // Player einfrieren
        player.isInteracting = true;

        // Panel sichtbar machen
        AlarmClockPanel.SetActive(true);

        // NEU: Alarm starten
        if (alarmAudio != null)
            alarmAudio.Play();
    }

    // Beendet die Interaktion (success entscheidet über Progression)
    private void EndInteraction(bool success)
    {
        isInteracting = false;

        // Player wieder freigeben
        player.isInteracting = false;

        // Panel ausblenden
        AlarmClockPanel.SetActive(false);

        // NEU: Alarm stoppen
        if (alarmAudio != null && alarmAudio.isPlaying)
            alarmAudio.Stop();

        // Nur bei erfolgreichem Klick Progression melden
        if (success)
        {
            OnInteractionCompleted?.Invoke();
        }
    }

    void Update()
    {
        // Eingaben nur erlauben, wenn Interaktion aktiv ist
        if (!isInteracting)
            return;

        // Mit F kann die Interaktion abgebrochen werden
        if (Input.GetKeyDown(KeyCode.F))
        {
            EndInteraction(false);
            return;
        }

        // Linksklick überprüft, ob der Wecker getroffen wurde
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            // Prüfen, ob der Klick auf dem Wecker gelandet ist
            if (hit != null && hit.gameObject == Wecker)
            {
                EndInteraction(true);
            }
        }
    }
}