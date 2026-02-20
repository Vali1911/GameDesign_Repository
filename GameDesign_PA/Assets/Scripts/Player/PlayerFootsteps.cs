using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Pitch Variation")]
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
        if (!audioSource.isPlaying)
        {
            audioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
            audioSource.Play();
        }
    }

    void StopFootsteps()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}