using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoTrigger : MonoBehaviour
{
    public GameObject blinkingArrow;
    public VideoPlayer videoPlayer;
    public ScreenFade screenFade;

    public Transform spawnPoint;
    public Transform cameraFocusTarget;

    public float freezeAfterFadeTime = 5f;

    [Header("Audio")]
    public AudioClip cutsceneClip;

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed) return;
        if (!other.CompareTag("Player")) return;

        hasPlayed = true;
        StartCoroutine(PlayCutscene(other));
    }

    private IEnumerator PlayCutscene(Collider2D playerCollider)
    {
        if (blinkingArrow != null)
            blinkingArrow.SetActive(false);

        PlayerMovement movement = playerCollider.GetComponent<PlayerMovement>();
        Rigidbody2D rb = playerCollider.GetComponent<Rigidbody2D>();
        Animator animator = playerCollider.GetComponent<Animator>();

        CameraFollow camFollow = Camera.main != null
            ? Camera.main.GetComponent<CameraFollow>()
            : null;

        // PLAYER EINFRIEREN
        if (movement != null) movement.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.enabled = false;

        // FADE TO BLACK
        if (screenFade != null)
            yield return screenFade.FadeOut();

        // KAMERA UMSCHALTEN
        if (camFollow != null && cameraFocusTarget != null)
            camFollow.SetTarget(cameraFocusTarget);

        // PLAYER TELEPORTIEREN
        if (spawnPoint != null)
            playerCollider.transform.position = spawnPoint.position;

        // ANDERE VIDEOS NUR PAUSIEREN (NICHT STOPPEN!)
        VideoPlayer[] allPlayers = FindObjectsOfType<VideoPlayer>();
        foreach (VideoPlayer vp in allPlayers)
        {
            if (vp != null && vp != videoPlayer)
            {
                vp.Pause();
            }
        }

        // CUTSCENE STARTEN
        if (videoPlayer != null)
        {
            videoPlayer.time = 0;
            videoPlayer.Play();
        }

        // CUTSCENE SOUND STARTEN
        if (AudioManager.Instance != null && cutsceneClip != null)
        {
            AudioManager.Instance.PlayLoopingSFX(cutsceneClip);
        }

        // FADE IN
        if (screenFade != null)
            yield return screenFade.FadeIn();

        // CUTSCENE-ZEIT
        yield return new WaitForSeconds(freezeAfterFadeTime);

        // SOUND STOPPEN
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopLoopingSFX();
        }

        // VIDEO ZURÜCK AUF POSTERFRAME
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += ResetToPosterFrame;
        }

        // KAMERA ZURÜCK ZUM PLAYER
        if (camFollow != null)
            camFollow.SetTarget(playerCollider.transform);

        // PLAYER WIEDER FREIGEBEN
        if (animator != null) animator.enabled = true;
        if (movement != null) movement.enabled = true;

        // TRIGGER DEAKTIVIEREN
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    private void ResetToPosterFrame(VideoPlayer vp)
    {
        vp.prepareCompleted -= ResetToPosterFrame;

        vp.Play();
        vp.Pause(); // Erstes Frame anzeigen
    }
}