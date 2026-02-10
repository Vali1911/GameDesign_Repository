using UnityEngine;

public class PanelRelockTrigger : MonoBehaviour
{
    // Der Collider, der den Rückweg blockiert (RightWall_Panel_5)
    public Collider2D barrierCollider;

    // Der Pfeil, der wieder ausgeblendet werden soll
    public GameObject arrowIndicator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Nur auf den Player reagieren
        if (!other.CompareTag("Player"))
            return;

        // Barrier wieder aktivieren
        if (barrierCollider != null)
            barrierCollider.enabled = true;

        // Pfeil wieder ausblenden
        if (arrowIndicator != null)
            arrowIndicator.SetActive(false);
    }
}
