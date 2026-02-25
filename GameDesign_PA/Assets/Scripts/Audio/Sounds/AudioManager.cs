using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxLoopSource;
    public AudioSource sfxOneShotSource;

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

    void ApplyVolumes()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume;

        if (sfxLoopSource != null)
            sfxLoopSource.volume = sfxVolume;

        if (sfxOneShotSource != null)
            sfxOneShotSource.volume = sfxVolume;
    }

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

    // ---------------- LOOPING ----------------

    public void PlayLoopingSFX(AudioClip clip)
    {
        if (clip == null || sfxLoopSource == null) return;

        sfxLoopSource.Stop();
        sfxLoopSource.clip = clip;
        sfxLoopSource.loop = true;
        sfxLoopSource.Play();
    }

    public void StopLoopingSFX()
    {
        if (sfxLoopSource == null) return;

        sfxLoopSource.Stop();
        sfxLoopSource.clip = null;
    }

    // ---------------- ONE SHOT ----------------

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxOneShotSource == null) return;

        sfxOneShotSource.PlayOneShot(clip);
    }

    // ---------------- STOP ALL ----------------

    public void StopAllSFX()
    {
        if (sfxLoopSource != null)
        {
            sfxLoopSource.Stop();
            sfxLoopSource.clip = null;
        }

        if (sfxOneShotSource != null)
        {
            sfxOneShotSource.Stop();
        }
    }

    public void PauseAllSFX()
    {
        if (sfxLoopSource != null && sfxLoopSource.isPlaying)
            sfxLoopSource.Pause();
    }

    public void ResumeAllSFX()
    {
        if (sfxLoopSource != null && sfxLoopSource.clip != null)
            sfxLoopSource.UnPause();
    }
}