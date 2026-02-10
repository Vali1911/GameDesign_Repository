using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    // Referenz auf ein UI-Image (Schwarz)
    public Image fadeImage;

    // Dauer des Fade-Effekts in Sekunden
    public float fadeDuration = 0.3f;

    // Öffentliche Coroutine für ein Fade-Out
    // (Bildschirm wird von transparent zu schwarz)
    public IEnumerator FadeOut()
    {
        // Startet den Fade von Alpha 0 (unsichtbar)
        // zu Alpha 1 (voll sichtbar)
        yield return Fade(0f, 1f);
    }

    // Öffentliche Coroutine für ein Fade-In
    // (Bildschirm wird von schwarz zu transparent)
    public IEnumerator FadeIn()
    {
        // Startet den Fade von Alpha 1 (voll sichtbar)
        // zu Alpha 0 (unsichtbar)
        yield return Fade(1f, 0f);
    }

    // Zentrale Fade-Logik
    // Diese Methode wird von FadeIn und FadeOut benutzt
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        // Zeitvariable die mitzählt wie lange der Fade bereits läuft
        float t = 0f;

        // Aktuelle Farbe des Fade-Images zwischenspeichern
        Color c = fadeImage.color;

        // Solange die Fade-Dauer noch nicht erreicht ist
        while (t < fadeDuration)
        {
            // Zeit fortschreiten lassen (frameabhängig)
            t += Time.deltaTime;

            // Alpha-Wert zwischen Start und Ziel interpolieren
            // t / fadeDuration ergibt einen Wert von 0 -> 1
            c.a = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);

            // Neue Farbe (mit geändertem Alpha) zurücksetzen
            fadeImage.color = c;

            // Warten bis zum nächsten Frame
            yield return null;
        }

        // Sicherheit: Am Ende exakt den Ziel-Alpha setzen
        c.a = endAlpha;
        fadeImage.color = c;
    }
}