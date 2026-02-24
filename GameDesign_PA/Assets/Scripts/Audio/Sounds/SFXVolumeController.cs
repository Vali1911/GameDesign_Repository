using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager fehlt!");
            return;
        }

        volumeSlider.value = AudioManager.Instance.sfxVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
