using UnityEngine;
using System.Collections;

public class FHintTrigger2D : MonoBehaviour
{
    [Header("F Hint")]
    public FHintSpritePulseFade fHint;

    [Header("Interaction Panel")]
    public GameObject panelToShow; // dein Panel (das aufploppt)

    [Header("Mouse Hint")]
    public MouseHintWiggle mouseHint;   // Maus-Sprite hier reinziehen
    public float mouseHintDelay = 3f;   // nach 3 Sekunden anzeigen

    private bool playerInside = false;
    private bool interactionStarted = false;
    private bool leftClicked = false;

    private Coroutine mouseRoutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;

        // F hint nur zeigen, wenn noch nicht benutzt
        if (fHint != null && !FHintSpritePulseFade.fAlreadyUsed)
            fHint.Show();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;

        if (fHint != null && !FHintSpritePulseFade.fAlreadyUsed)
            fHint.Hide();

        // reset interaction state when leaving trigger
        interactionStarted = false;
        leftClicked = false;

        if (mouseRoutine != null)
        {
            StopCoroutine(mouseRoutine);
            mouseRoutine = null;
        }

        if (mouseHint != null)
            mouseHint.Hide();
    }

    private void Update()
    {
        if (!playerInside) return;

        // Start interaction with F
        if (!interactionStarted && Input.GetKeyDown(KeyCode.F))
        {
            interactionStarted = true;
            leftClicked = false;

            // Panel show
            if (panelToShow != null)
                panelToShow.SetActive(true);

            // F hint disappears forever
            if (fHint != null)
            {
                FHintSpritePulseFade.fAlreadyUsed = true;
                fHint.Hide();
            }

            // Start timer for mouse hint
            if (mouseRoutine != null) StopCoroutine(mouseRoutine);
            mouseRoutine = StartCoroutine(ShowMouseHintAfterDelay());
        }

        // If player left-clicks, hide mouse hint and mark as clicked
        if (interactionStarted && Input.GetMouseButtonDown(0))
        {
            leftClicked = true;

            if (mouseHint != null)
                mouseHint.Hide();
        }
    }

    private IEnumerator ShowMouseHintAfterDelay()
    {
        yield return new WaitForSeconds(mouseHintDelay);

        // Only show if still in interaction and no click happened
        if (!interactionStarted) yield break;
        if (!playerInside) yield break;
        if (leftClicked) yield break;

        if (mouseHint == null) yield break;

        // IMPORTANT: if mouse object is inactive, activate it first (avoids coroutine error)
        if (!mouseHint.gameObject.activeSelf)
            mouseHint.gameObject.SetActive(true);

        mouseHint.Show();
    }
}