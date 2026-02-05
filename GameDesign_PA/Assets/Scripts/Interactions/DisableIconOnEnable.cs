using UnityEngine;

public class DisableIconOnEnable : MonoBehaviour
{
    // Referenz auf das Interaktions-Icon (Icon2)
    public GameObject interactionIcon;

    // Referenz auf den PanelBarrierController (z. B. von Panel_03_Bossroom)
    public PanelBarrierController panelBarrierController;

    void OnEnable()
    {
        // Sobald die Sprechblase aktiviert wird,
        // wird das Interaktions-Icon ausgeblendet
        if (interactionIcon != null)
        {
            // 🔔 Panel informieren, DANN deaktivieren
            if (panelBarrierController != null)
            {
                panelBarrierController.OnIconHidden(interactionIcon);
            }

            interactionIcon.SetActive(false);
        }
    }
}
