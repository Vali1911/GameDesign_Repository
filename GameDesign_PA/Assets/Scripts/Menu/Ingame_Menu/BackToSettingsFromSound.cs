using UnityEngine;
using UnityEngine.EventSystems;

public class BackToSettingsFromSound : MonoBehaviour, IPointerUpHandler
{
    public GameObject settingsMenu;
    public GameObject soundsMenu;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(true);

        if (soundsMenu != null)
            soundsMenu.SetActive(false);
    }
}

