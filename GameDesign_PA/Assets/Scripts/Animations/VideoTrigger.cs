using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public ScreenFade screenFade;

    public Transform spawnPoint;
    public Transform cameraFocusTarget;   // Die Plane
    public float freezeAfterFadeTime = 5f;

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
        PlayerMovement movement = playerCollider.GetComponent<PlayerMovement>();
        Rigidbody2D rb = playerCollider.GetComponent<Rigidbody2D>();
        Animator animator = playerCollider.GetComponent<Animator>();

        CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();

        // Player einfrieren
        if (movement != null) movement.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.enabled = false;

        // Fade to black
        yield return screenFade.FadeOut();

        // Kamera auf Video-Plane richten
        if (camFollow != null && cameraFocusTarget != null)
            camFollow.SetTarget(cameraFocusTarget);

        // Teleport im Schwarzen
        if (spawnPoint != null)
            playerCollider.transform.position = spawnPoint.position;

        // Video starten
        if (videoPlayer != null)
            videoPlayer.Play();

        // Fade zurueck
        yield return screenFade.FadeIn();

        // Kamera bleibt 5 Sekunden auf der Plane
        yield return new WaitForSeconds(freezeAfterFadeTime);

        // Kamera folgt wieder dem Player
        if (camFollow != null)
            camFollow.SetTarget(playerCollider.transform);

        // Player freigeben
        if (animator != null) animator.enabled = true;
        if (movement != null) movement.enabled = true;

        GetComponent<Collider2D>().enabled = false;
    }
}