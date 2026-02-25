
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Footstep Settings")]
    public AudioClip footstepClip;
    public float pitchVariation = 0.05f;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerMovement == null || rb == null || AudioManager.Instance == null)
            return;

        // Kein Sound wenn Menü offen
        if (InGameMenu.IsGamePaused)
        {
            StopFootsteps();
            return;
        }

        if (playerMovement.isInteracting)
        {
            StopFootsteps();
            return;
        }

        if (IsMoving())
            StartFootsteps();
        else
            StopFootsteps();
    }

    bool IsMoving()
    {
        return Mathf.Abs(rb.linearVelocity.x) > 0.1f;
    }

    void StartFootsteps()
    {
        if (footstepClip == null)
            return;

        var loopSource = AudioManager.Instance.sfxLoopSource;

        if (loopSource == null)
            return;

        if (loopSource.clip == footstepClip && loopSource.isPlaying)
            return;

        loopSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);

        AudioManager.Instance.PlayLoopingSFX(footstepClip);
    }

    void StopFootsteps()
    {
        var loopSource = AudioManager.Instance.sfxLoopSource;

        if (loopSource != null && loopSource.clip == footstepClip)
        {
            AudioManager.Instance.StopLoopingSFX();
        }
    }
}