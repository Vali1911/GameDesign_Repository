using System.Collections.Generic;
using UnityEngine;

public class PanelBarrierController : MonoBehaviour
{
    // Collider, der den Weg ins nächste Panel blockiert
    public Collider2D barrierCollider;

    // Blinkender Pfeil, der anzeigt, dass man weitergehen kann
    public GameObject arrowIndicator;

    // Alle Icons, die zu diesem Panel gehören
    // Diese Liste wird im Inspector gefüllt
    public List<GameObject> iconsInPanel = new List<GameObject>();

    // Verhindert mehrfaches Abschließen des Panels
    private bool panelCompleted = false;

    private void Start()
    {
        // Zu Beginn ist der Weg blockiert
        if (barrierCollider != null)
            barrierCollider.enabled = true;

        // Pfeil ist anfangs unsichtbar
        if (arrowIndicator != null)
            arrowIndicator.SetActive(false);
    }

    /// Wird aufgerufen, wenn ein Icon ausgeblendet wurde
    public void OnIconHidden(GameObject icon)
    {
        if (panelCompleted)
            return;

        if (!iconsInPanel.Contains(icon))
            return;

        iconsInPanel.Remove(icon);

        // Wenn keine Icons mehr übrig sind -> Panel abgeschlossen
        if (iconsInPanel.Count == 0)
        {
            CompletePanel();
        }
    }

    /// Panel ist abgeschlossen → Weg freigeben
    private void CompletePanel()
    {
        panelCompleted = true;

        // Barrier deaktivieren -> Spieler kann weitergehen
        if (barrierCollider != null)
            barrierCollider.enabled = false;

        // Blinkenden Pfeil anzeigen
        if (arrowIndicator != null)
            arrowIndicator.SetActive(true);
    }

    /// Wird aufgerufen, wenn der Spieler das Panel verlässt
    /*
    public void OnPlayerLeftPanel()
    {
        // Pfeil wieder ausblenden
        if (arrowIndicator != null)
            arrowIndicator.SetActive(false);

        // Barrier wieder aktivieren (Rückweg sperren)
        if (barrierCollider != null)
            barrierCollider.enabled = true;
    }
    */
}

