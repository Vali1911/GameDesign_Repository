using UnityEngine;
using UnityEngine.UI;

public class StartMusicSettings : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Slider volumeSlider;

    private const string VolumeKey = "MusicVolume";

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);

        backgroundMusic.volume = savedVolume;
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat(VolumeKey, volume);
    }
}