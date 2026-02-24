using UnityEngine;
using UnityEngine.UI;

public class InGameMusicSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void OnEnable()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager fehlt!");
            return;
        }

        volumeSlider.value = AudioManager.Instance.musicVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value);
    }
}