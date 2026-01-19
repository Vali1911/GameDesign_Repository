using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public ScreenFade screenFade;

    public Transform spawnPoint;
    public Transform cameraFocusTarget;
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

        CameraFollow camFollow = Camera.main != null ? Camera.main.GetComponent<CameraFollow>() : null;

        // Freeze player
        if (movement != null) movement.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.enabled = false;

        // Fade to black
        if (screenFade != null)
            yield return screenFade.FadeOut();

        // Switch camera to cutscene target
        if (camFollow != null && cameraFocusTarget != null)
            camFollow.SetTarget(cameraFocusTarget);

        // Teleport while black
        if (spawnPoint != null)
            playerCollider.transform.position = spawnPoint.position;

        // Stop all other videos so only one can play
        if (videoPlayer != null)
        {
            VideoPlayer[] allPlayers = FindObjectsOfType<VideoPlayer>();
            for (int i = 0; i < allPlayers.Length; i++)
            {
                if (allPlayers[i] != null && allPlayers[i] != videoPlayer)
                    allPlayers[i].Stop();
            }
        }

        // Start this video
        if (videoPlayer != null)
            videoPlayer.Play();

        // Fade back in
        if (screenFade != null)
            yield return screenFade.FadeIn();

        // Keep camera on cutscene for some time
        yield return new WaitForSeconds(freezeAfterFadeTime);

        // Switch camera back to player
        if (camFollow != null)
            camFollow.SetTarget(playerCollider.transform);

        // Unfreeze player
        if (animator != null) animator.enabled = true;
        if (movement != null) movement.enabled = true;

        // Disable this trigger so it cannot be triggered again
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }
}