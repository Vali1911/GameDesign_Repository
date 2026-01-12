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

    void Start()
    {
        // Zu Beginn ist der Weg blockiert
        if (barrierCollider != null)
            barrierCollider.enabled = true;

        // Pfeil ist anfangs unsichtbar
        if (arrowIndicator != null)
            arrowIndicator.SetActive(false);
    }

    // Diese Methode wird über ein UnityEvent aufgerufen,
    // sobald ein Icon ausgeblendet wird
    public void OnIconHidden(GameObject icon)
    {
        // Falls das Panel bereits abgeschlossen ist, abbrechen
        if (panelCompleted)
            return;

        // Sicherheitscheck
        if (!iconsInPanel.Contains(icon))
            return;

        // Entfernt das Icon aus der Panel-Liste
        iconsInPanel.Remove(icon);

        // Wenn keine Icons mehr übrig sind, ist das Panel abgeschlossen
        if (iconsInPanel.Count == 0)
        {
            CompletePanel();
        }
    }

    // Wird genau einmal aufgerufen, wenn alle Icons weg sind
    private void CompletePanel()
    {
        panelCompleted = true;

        // Barrier deaktivieren → Weg freigeben
        if (barrierCollider != null)
            barrierCollider.enabled = false;

        // Blinkenden Pfeil anzeigen
        if (arrowIndicator != null)
            arrowIndicator.SetActive(true);
    }

    // Wird aufgerufen, wenn der Spieler ins nächste Panel läuft
    public void OnPlayerLeftPanel()
    {
        // Pfeil wieder ausblenden
        if (arrowIndicator != null)
            arrowIndicator.SetActive(false);
    }
}
