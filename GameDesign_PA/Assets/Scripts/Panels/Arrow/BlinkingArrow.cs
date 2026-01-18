using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Zeit, wie lange der Pfeil sichtbar ist
    public float visibleTime = 0.5f;

    // Zeit, wie lange der Pfeil unsichtbar ist
    public float hiddenTime = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Coroutine blinkRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // Startet das Blinken, sobald das Objekt aktiv wird
        blinkRoutine = StartCoroutine(Blink());
    }

    void OnDisable()
    {
        // Stoppt das Blinken, wenn das Objekt deaktiviert wird
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        // Sicherheit: Pfeil ausblenden
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(hiddenTime);
        }
    }
}
