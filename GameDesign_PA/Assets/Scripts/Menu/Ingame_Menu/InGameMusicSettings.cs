using UnityEngine;
using UnityEngine.UI;

public class InGameMusicSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void OnEnable()
    {
        AudioSource music = MusicManager.Instance.backgroundMusic;

        volumeSlider.value = music.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        MusicManager.Instance.backgroundMusic.volume = volume;
    }
}

