using UnityEngine;
using UnityEngine.Events;

public class DialogueInteraction : MonoBehaviour
{
    [Header("Core")]
    public PlayerMovement player;
    public GameObject interactionIcon;

    [Header("Dialogue")]
    public GameObject[] dialogueBubbles; // Reihenfolge im Inspector!
    private int currentIndex = 0;

    [Header("Events")]
    public UnityEvent OnDialogueCompleted;

    private bool isInteracting = false;
    private bool completed = false;

    // Wird vom InteractiveObject aufgerufen
    public void StartDialogue()
    {
        if (completed || isInteracting)
            return;

        isInteracting = true;
        player.isInteracting = true;

        gameObject.SetActive(true);
        ShowBubble(0);
    }

    void Update()
    {
        if (!isInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            AdvanceDialogue();
        }
    }

    private void AdvanceDialogue()
    {
        // Aktuelle Bubble ausblenden
        dialogueBubbles[currentIndex].SetActive(false);
        currentIndex++;

        // Gibt es noch eine nächste?
        if (currentIndex < dialogueBubbles.Length)
        {
            ShowBubble(currentIndex);
        }
        else
        {
            CompleteDialogue();
        }
    }

    private void ShowBubble(int index)
    {
        dialogueBubbles[index].SetActive(true);
    }

    private void CompleteDialogue()
    {
        completed = true;
        isInteracting = false;

        player.isInteracting = false;

        // Progression melden
        OnDialogueCompleted?.Invoke();

        // Icon ausblenden
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        gameObject.SetActive(false);
    }
}
