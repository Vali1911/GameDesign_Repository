using UnityEngine;

public enum PanelType
{
    Gameplay,
    Cutscene
}

public class PanelController : MonoBehaviour
{
    // Panel Type
    public PanelType panelType;

    // Gameplay Panel
    public PanelBarrierController barrierController;

    // Cutscene Panel
    public CutsceneController cutsceneController;

    // Flow
    public PanelController nextPanel;
    public Transform spawnPointInNextPanel;

    public void OnPlayerEntered(GameObject player)
    {
        // Rückweg sperren (Pfeil aus, Barrier an)
        if (barrierController != null)
            barrierController.OnPlayerLeftPanel();

        if (panelType == PanelType.Gameplay)
        {
            // Nichts Besonderes -> Gameplay läuft
            return;
        }

        if (panelType == PanelType.Cutscene)
        {
            // Player ausblenden & blockieren
            player.SetActive(false);

            // Cutscene starten
            cutsceneController.Play(() =>
            {
                // Nach Cutscene:
                player.transform.position = spawnPointInNextPanel.position;
                player.SetActive(true);
            });
        }
    }
}