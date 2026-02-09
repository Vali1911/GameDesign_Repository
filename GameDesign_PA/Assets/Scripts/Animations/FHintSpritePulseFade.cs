using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class FHintSpritePulseFade : MonoBehaviour
{
    // ===== GLOBAL =====
    // Merkt sich: Wurde F schon einmal gedrückt?
    public static bool fAlreadyUsed = false;

    [Header("Input")]
    public KeyCode keyToHide = KeyCode.F;

    [Header("Pulse")]
    public float pulseSpeed = 2.5f;
    public float pulseAmount = 0.12f;

    [Header("Fade")]
    public float showDelay = 1.0f;
    public float fadeInTime = 1.0f;
    public float fadeOutTime = 0.8f;

    private SpriteRenderer sr;
    private Vector3 baseScale;

    private bool isVisible = false;
    private bool isFading = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;

        SetAlpha(0f);
        isVisible = false;
    }

    void Update()
    {
        // Wenn F schon benutzt wurde → komplett ignorieren
        if (fAlreadyUsed)
            return;

        if (!isVisible || isFading)
            return;

        // Pulse
        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f;
        float scale = 1f + (t - 0.5f) * 2f * pulseAmount;
        transform.localScale = baseScale * scale;

        // F gedrückt → für immer ausblenden
        if (Input.GetKeyDown(keyToHide))
        {
            fAlreadyUsed = true;
            Hide();
        }
    }

    // ===== PUBLIC =====

    public void Show()
    {
        // Wenn F schon benutzt wurde → NICHT mehr anzeigen
        if (fAlreadyUsed)
            return;

        StopAllCoroutines();
        transform.localScale = baseScale;
        StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        if (!gameObject.activeInHierarchy)
            return;

        StopAllCoroutines();
        StartCoroutine(FadeTo(0f, fadeOutTime, false));
    }

    // ===== ROUTINES =====

    private IEnumerator ShowRoutine()
    {
        isFading = true;
        SetAlpha(0f);
        isVisible = false;

        if (showDelay > 0f)
            yield return new WaitForSeconds(showDelay);

        yield return FadeTo(1f, fadeInTime, true);

        isFading = false;
    }

    private IEnumerator FadeTo(float targetAlpha, float duration, bool makeVisible)
    {
        float startAlpha = sr.color.a;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(targetAlpha);
        isVisible = makeVisible;
    }

    private void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = Mathf.Clamp01(a);
        sr.color = c;
    }
}