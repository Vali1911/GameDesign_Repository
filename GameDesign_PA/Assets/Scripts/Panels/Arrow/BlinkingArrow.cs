using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Dauer, wie lange der Pfeil sichtbar bleibt
    public float visibleTime = 0.5f;

    // Dauer, wie lange der Pfeil ausgeblendet bleibt
    public float hiddenTime = 0.5f;

    // Referenz auf den SpriteRenderer des Pfeils
    private SpriteRenderer spriteRenderer;

    // Speichert die aktuell laufende Coroutine
    private Coroutine blinkRoutine;

    void Awake()
    {
        // SpriteRenderer beim Start zwischenspeichern
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // Blinken starten, sobald das Objekt aktiviert wird
        blinkRoutine = StartCoroutine(Blink());
    }

    void OnDisable()
    {
        // Laufende Blink-Coroutine stoppen
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        // Sicherheitshalber Sprite ausblenden
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }

    // Coroutine für das periodische Ein- und Ausblenden
    IEnumerator Blink()
    {
        while (true)
        {
            // Pfeil sichtbar machen
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            // Pfeil ausblenden
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(hiddenTime);
        }
    }
}
