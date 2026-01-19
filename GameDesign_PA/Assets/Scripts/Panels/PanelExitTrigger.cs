/*using System.Collections;
using UnityEngine;

public class PanelExitTrigger : MonoBehaviour
{
    // =========================
    // Kamera
    // =========================

    public CameraFollow cameraFollow;
    public Transform cutsceneCameraFocus;

    // =========================
    // Panel-Wechsel
    // =========================

    public Transform nextPanelSpawnPoint;

    // =========================
    // Player
    // =========================

    public GameObject player;

    // =========================
    // Cutscene
    // =========================

    public GameObject cutsceneRoot;    // DAS Cutscene-Panel / Objekt
    public float cutsceneDuration = 2f;

    // =========================
    // Intern
    // =========================

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered)
            return;

        if (!collision.CompareTag("Player"))
            return;

        hasTriggered = true;
        StartCoroutine(HandlePanelExit());
    }

    private IEnumerator HandlePanelExit()
    {
        // 1️⃣ Kamera auf Cutscene zentrieren
        cameraFollow.SetTarget(cutsceneCameraFocus);

        // 2️⃣ Playerbewegung sperren (NICHT deaktivieren!)
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        // 3️⃣ Cutscene STARTEN (wie früher!)
        if (cutsceneRoot != null)
            cutsceneRoot.SetActive(true);

        // 4️⃣ Warten bis Cutscene vorbei ist
        yield return new WaitForSeconds(cutsceneDuration);

        // 5️⃣ Cutscene wieder deaktivieren (optional, aber sauber)
        if (cutsceneRoot != null)
            cutsceneRoot.SetActive(false);

        // 6️⃣ Player ins nächste Panel setzen
        player.transform.position = nextPanelSpawnPoint.position;

        // 7️⃣ Playerbewegung wieder aktivieren
        if (movement != null)
            movement.enabled = true;

        // 8️⃣ Kamera folgt wieder dem Player
        cameraFollow.SetTarget(player.transform);
    }
}
*/