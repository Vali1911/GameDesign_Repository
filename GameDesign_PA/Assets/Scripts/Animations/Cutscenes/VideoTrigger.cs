using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoTrigger : MonoBehaviour
{
    // Referenz auf den Pfeil
    public GameObject blinkingArrow;

    // Referenz auf den VideoPlayer
    public VideoPlayer videoPlayer;

    // Referenz auf das ScreenFade-Script
    public ScreenFade screenFade;

    // Position, an die der Player waehrend der Cutscene teleportiert wird
    public Transform spawnPoint;

    // Zielpunkt fuer die Kamera waehrend der Cutscene
    public Transform cameraFocusTarget;

    // Zeitspanne, die die Kamera nach dem Fade-In noch auf der Cutscene bleibt
    public float freezeAfterFadeTime = 5f;

    // Merker, damit die Cutscene nur EINMAL abgespielt wird
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Falls die Cutscene bereits abgespielt wurde -> abbrechen
        if (hasPlayed) return;

        // Nur der Player darf die Cutscene ausloesen
        if (!other.CompareTag("Player")) return;

        // Markieren, dass die Cutscene jetzt gespielt wird
        hasPlayed = true;

        // Startet die komplette Cutscene-Abfolge als Coroutine
        StartCoroutine(PlayCutscene(other));
    }

    private IEnumerator PlayCutscene(Collider2D playerCollider)
    {
        // BlinkingArrow ausblenden, sobald die Cutscene startet
        if (blinkingArrow != null)
            blinkingArrow.SetActive(false);

        // Referenzen auf typische Player-Komponenten holen
        PlayerMovement movement = playerCollider.GetComponent<PlayerMovement>();
        Rigidbody2D rb = playerCollider.GetComponent<Rigidbody2D>();
        Animator animator = playerCollider.GetComponent<Animator>();

        // Referenz auf das CameraFollow-Script der MainCamera holen
        CameraFollow camFollow = Camera.main != null
            ? Camera.main.GetComponent<CameraFollow>()
            : null;

        // =========================
        // PLAYER EINFRIEREN
        // =========================

        if (movement != null) movement.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.enabled = false;

        // =========================
        // FADE TO BLACK
        // =========================

        if (screenFade != null)
            yield return screenFade.FadeOut();

        // =========================
        // KAMERA UMSCHALTEN
        // =========================

        if (camFollow != null && cameraFocusTarget != null)
            camFollow.SetTarget(cameraFocusTarget);

        // =========================
        // PLAYER TELEPORTIEREN
        // =========================

        if (spawnPoint != null)
            playerCollider.transform.position = spawnPoint.position;

        // =========================
        // ANDERE VIDEOS STOPPEN
        // =========================

        if (videoPlayer != null)
        {
            VideoPlayer[] allPlayers = FindObjectsOfType<VideoPlayer>();
            for (int i = 0; i < allPlayers.Length; i++)
            {
                if (allPlayers[i] != null && allPlayers[i] != videoPlayer)
                    allPlayers[i].Stop();
            }
        }

        // =========================
        // CUTSCENE STARTEN
        // =========================

        // Wichtig fuer Poster-Frame Setup:
        // - sicherstellen, dass das Video von vorne startet
        // - Play auf jeden Fall aus dem Pause-Standbild heraus startet
        if (videoPlayer != null)
        {
            videoPlayer.time = 0;
            videoPlayer.Play();
        }

        // =========================
        // FADE IN
        // =========================

        if (screenFade != null)
            yield return screenFade.FadeIn();

        // =========================
        // CUTSCENE-ZEIT
        // =========================

        yield return new WaitForSeconds(freezeAfterFadeTime);

        // =========================
        // KAMERA ZURUECK ZUM PLAYER
        // =========================

        if (camFollow != null)
            camFollow.SetTarget(playerCollider.transform);

        // =========================
        // PLAYER WIEDER FREIGEBEN
        // =========================

        if (animator != null) animator.enabled = true;
        if (movement != null) movement.enabled = true;

        // =========================
        // TRIGGER DEAKTIVIEREN
        // =========================

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }
}