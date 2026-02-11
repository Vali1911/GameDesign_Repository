using System.Collections;
using UnityEngine;

public class AlarmClockAutoRing : MonoBehaviour
{
    [Header("Reference")]
    public Transform clockVisual; // sichtbare Uhr (Sprite)

    [Header("Interaction")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.F;

    [Header("Timing")]
    public float shakeTime = 0.35f; // wie lange wackeln
    public float restTime = 0.25f;  // Pause zwischen Wackeln

    [Header("Shake Settings")]
    public float speed = 70f;       // Wackel-Geschwindigkeit
    public float angle = 14f;       // Wackel-Stärke (Grad)

    private bool playerInRange;
    private bool hasActivated;      // F nur EINMAL auslösen
    private Quaternion originalRotation;
    private Coroutine ringCoroutine;

    private void Awake()
    {
        if (clockVisual != null)
            originalRotation = clockVisual.localRotation;
    }

    private void Update()
    {
        if (!playerInRange || hasActivated) return;

        if (Input.GetKeyDown(interactKey))
        {
            hasActivated = true;
            ringCoroutine = StartCoroutine(RingLoop());
        }
    }

    private IEnumerator RingLoop()
    {
        while (true)
        {
            // WACKELN
            float t = 0f;
            while (t < shakeTime)
            {
                t += Time.deltaTime;
                float z = Mathf.Sin(t * speed) * angle;
                clockVisual.localRotation =
                    originalRotation * Quaternion.Euler(0f, 0f, z);
                yield return null;
            }

            // NORMAL (Pause)
            clockVisual.localRotation = originalRotation;
            yield return new WaitForSeconds(restTime);
        }
    }

    //  Trigger 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = false;
    }

    //  Von der Uhr per Klick aufrufbar 
    public void StopRingingExternally()
    {
        hasActivated = false;

        if (ringCoroutine != null)
            StopCoroutine(ringCoroutine);

        if (clockVisual != null)
            clockVisual.localRotation = originalRotation;
    }
}

