using UnityEngine;
using UnityEngine.UI;

public class InGameMusicSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void OnEnable()
    {
        volumeSlider.value = MusicManager.Instance.backgroundMusic.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        MusicManager.Instance.SetVolume(volume);
    }
}
