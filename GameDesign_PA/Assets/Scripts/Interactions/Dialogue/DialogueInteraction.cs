using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DialogueInteraction : MonoBehaviour
{
    // Referenz auf den Player für Freeze-Logik
    public PlayerMovement player;

    // Icon, das den Dialog auslöst
    public GameObject interactionIcon;

    // Alle Sprechblasen in korrekter Reihenfolge
    public GameObject[] dialogueBubbles;

    // Event für Progression nach abgeschlossenem Dialog
    public UnityEvent OnDialogueCompleted;

    // Aktueller Index der angezeigten Sprechblase
    private int currentIndex = 0;

    // Gibt an, ob der Dialog aktuell läuft
    private bool isInteracting = false;

    // Verhindert mehrfaches Abschließen des Dialogs
    private bool completed = false;

    // Verhindert, dass der Start-Input direkt weiterblättert
    private bool canAdvance = false;

    // Wird vom InteractiveObject aufgerufen, wenn F im Icon gedrückt wird
    public void StartDialogue()
    {
        // Dialog nicht erneut starten, wenn bereits abgeschlossen oder aktiv
        if (completed || isInteracting)
            return;

        isInteracting = true;

        // Player einfrieren
        player.isInteracting = true;

        currentIndex = 0;

        // Panel aktivieren und erste Sprechblase anzeigen
        gameObject.SetActive(true);
        ShowBubble(currentIndex);

        // Einen Frame warten, bevor Weiterklicken erlaubt ist
        canAdvance = false;
        StartCoroutine(EnableAdvanceNextFrame());
    }

    // Aktiviert das Weiterblättern im nächsten Frame
    private IEnumerator EnableAdvanceNextFrame()
    {
        yield return null;
        canAdvance = true;
    }

    void Update()
    {
        // Eingabe nur erlauben, wenn Dialog aktiv ist
        if (!isInteracting)
            return;

        // Mit F zur nächsten Sprechblase wechseln
        if (canAdvance && Input.GetKeyDown(KeyCode.F))
        {
            AdvanceDialogue();
        }
    }

    // Blendet aktuelle Bubble aus und zeigt die nächste an
    private void AdvanceDialogue()
    {
        dialogueBubbles[currentIndex].SetActive(false);

        currentIndex++;

        // Wenn noch Bubbles vorhanden sind -> nächste anzeigen
        if (currentIndex < dialogueBubbles.Length)
        {
            ShowBubble(currentIndex);
        }
        else
        {
            CompleteDialogue();
        }
    }

    // Aktiviert eine bestimmte Sprechblase
    private void ShowBubble(int index)
    {
        if (dialogueBubbles[index] != null)
            dialogueBubbles[index].SetActive(true);
    }

    // Beendet den Dialog vollständig
    private void CompleteDialogue()
    {
        isInteracting = false;
        completed = true;

        // Player wieder freigeben
        player.isInteracting = false;

        // Progression melden
        OnDialogueCompleted?.Invoke();

        // Icon dauerhaft deaktivieren
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        gameObject.SetActive(false);
    }
}

