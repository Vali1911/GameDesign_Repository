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

        // 🔴 Keine Footsteps wenn Menü offen
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
        {
            StartFootsteps();
        }
        else
        {
            StopFootsteps();
        }
    }

    bool IsMoving()
    {
        return Mathf.Abs(rb.linearVelocity.x) > 0.1f;
    }

    void StartFootsteps()
    {
        if (AudioManager.Instance.sfxSource.clip == footstepClip &&
            AudioManager.Instance.sfxSource.isPlaying)
            return;

        AudioManager.Instance.sfxSource.pitch =
            1f + Random.Range(-pitchVariation, pitchVariation);

        AudioManager.Instance.PlayLoopingSFX(footstepClip);
    }

    void StopFootsteps()
    {
        if (AudioManager.Instance.sfxSource.clip == footstepClip)
        {
            AudioManager.Instance.StopLoopingSFX();
        }
    }
}