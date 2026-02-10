using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class MouseHintWiggle : MonoBehaviour
{
    [Header("Fade")]
    public float fadeInTime = 0.4f;
    public float fadeOutTime = 0.25f;

    [Header("Wiggle")]
    public float wiggleDistance = 0.15f;
    public float wiggleSpeed = 3f;

    private SpriteRenderer sr;
    private Vector3 basePos;
    private Coroutine currentRoutine;
    private bool visible = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        basePos = transform.localPosition;

        // WICHTIG: Objekt bleibt aktiv, nur unsichtbar
        SetAlpha(0f);
        visible = false;
    }

    void Update()
    {
        if (!visible) return;

        float x = Mathf.Sin(Time.time * wiggleSpeed) * wiggleDistance;
        transform.localPosition = basePos + new Vector3(x, 0f, 0f);
    }

    public void Show()
    {
        if (!gameObject.activeInHierarchy) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeTo(1f, fadeInTime, true));
    }

    public void Hide()
    {
        if (!gameObject.activeInHierarchy) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeTo(0f, fadeOutTime, false));
    }

    private IEnumerator FadeTo(float target, float duration, bool makeVisible)
    {
        float start = sr.color.a;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(start, target, t / duration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(target);
        visible = makeVisible && target > 0.01f;

        if (!visible)
            transform.localPosition = basePos;
    }

    private void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = Mathf.Clamp01(a);
        sr.color = c;
    }
}