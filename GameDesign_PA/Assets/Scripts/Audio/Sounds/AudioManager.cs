using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        ApplyVolumes();
    }

    // ------------------------
    // VOLUME SETTERS
    // ------------------------

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        ApplyVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        ApplyVolumes();
    }

    void ApplyVolumes()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume;

        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    // ------------------------
    // MUSIC
    // ------------------------

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource == null || clip == null) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    // ------------------------
    // ONE SHOT SFX
    // ------------------------

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // ------------------------
    // LOOPING SFX
    // ------------------------

    public void PlayLoopingSFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.Stop();
        sfxSource.clip = clip;
        sfxSource.loop = true;
        sfxSource.volume = sfxVolume;
        sfxSource.Play();
    }

    public void StopLoopingSFX()
    {
        if (sfxSource == null) return;

        sfxSource.loop = false;
        sfxSource.Stop();
    }

    // ------------------------
    // PAUSE / RESUME
    // ------------------------

    public void PauseAllSFX()
    {
        if (sfxSource != null && sfxSource.isPlaying)
            sfxSource.Pause();
    }

    public void ResumeAllSFX()
    {
        if (sfxSource != null && sfxSource.clip != null)
            sfxSource.UnPause();
    }

    // ------------------------
    // STOP (für Szenenwechsel)
    // ------------------------

    public void StopAllSFX()
    {
        if (sfxSource == null) return;

        sfxSource.Stop();
        sfxSource.clip = null;
    }
}