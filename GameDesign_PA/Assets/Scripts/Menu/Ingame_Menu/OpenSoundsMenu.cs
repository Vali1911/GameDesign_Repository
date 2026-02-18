using UnityEngine;
using UnityEngine.EventSystems;

public class OpenSoundsMenu : MonoBehaviour, IPointerUpHandler
{
    // Menus
    public GameObject settingsMenu;
    public GameObject soundsMenu;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (soundsMenu != null)
            soundsMenu.SetActive(true);

        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }
}

