using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    [Header("Settings")]
    public float stepInterval = 0.4f; // Zeit zwischen Schritten

    private float timer;

    void Update()
    {
        bool isMoving =
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D);

        if (isMoving)
        {
            timer += Time.deltaTime;

            if (timer >= stepInterval)
            {
                PlayFootstep();
                timer = 0f;
            }
        }
        else
        {
            timer = stepInterval; // sofort Sound beim Loslaufen
        }
    }

    void PlayFootstep()
    {
        if (audioSource == null || footstepClips.Length == 0) return;

        AudioClip clip =
            footstepClips[Random.Range(0, footstepClips.Length)];

        audioSource.PlayOneShot(clip);
    }
}