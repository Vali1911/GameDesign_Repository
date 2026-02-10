using UnityEngine;
using UnityEngine.EventSystems;

public class OpenSettingsMenu : MonoBehaviour, IPointerUpHandler
{
    [Header("UI Panels")]
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);

        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
    }
}
