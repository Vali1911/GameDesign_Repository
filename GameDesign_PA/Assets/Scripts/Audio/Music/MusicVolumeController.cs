using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (AudioManager.Instance == null) return;

        volumeSlider.value = AudioManager.Instance.musicVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }
}

