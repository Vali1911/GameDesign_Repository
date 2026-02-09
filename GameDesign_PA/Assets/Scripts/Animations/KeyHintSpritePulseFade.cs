using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class KeyHintSpritePulseFade : MonoBehaviour
{
    public KeyCode keyToHide = KeyCode.A;

    [Header("Pulse")]
    public float pulseSpeed = 2.5f;
    public float pulseAmount = 0.12f;

    [Header("Fade")]
    public float startDelay = 1.0f;
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 1.5f;
    public bool disableAfterFadeOut = true;

    private SpriteRenderer sr;
    private Vector3 baseScale;

    private bool isFading = false;
    private bool isHidden = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
    }

    void OnEnable()
    {
        StopAllCoroutines();

        isHidden = false;
        isFading = true;

        SetAlpha(0f);
        transform.localScale = baseScale;

        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        yield return FadeTo(1f, fadeInTime);

        isFading = false;
    }

    void Update()
    {
        if (isHidden || isFading) return;

        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f;
        float s = 1f + (t - 0.5f) * 2f * pulseAmount;
        transform.localScale = baseScale * s;

        if (Input.GetKeyDown(keyToHide))
        {
            StartCoroutine(FadeOutAndHide());
        }
    }

    private IEnumerator FadeOutAndHide()
    {
        isFading = true;
        transform.localScale = baseScale;

        yield return FadeTo(0f, fadeOutTime);

        isHidden = true;

        if (disableAfterFadeOut)
            gameObject.SetActive(false);
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = sr.color.a;

        if (duration <= 0f)
        {
            SetAlpha(targetAlpha);
            yield break;
        }

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = Mathf.Clamp01(a);
        sr.color = c;
    }
}
