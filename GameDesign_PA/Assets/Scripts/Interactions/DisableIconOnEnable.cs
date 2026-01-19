using UnityEngine;

public class DisableIconOnEnable : MonoBehaviour
{
    // Referenz auf das Interaktions-Icon (mit InteractiveObject)
    public GameObject interactionIcon;

    void OnEnable()
    {
        // Sobald die Sprechblase aktiviert wird,
        // wird das Interaktions-Icon ausgeblendet
        if (interactionIcon != null)
            interactionIcon.SetActive(false);
    }
}
