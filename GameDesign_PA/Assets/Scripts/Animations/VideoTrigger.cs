using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoTrigger : MonoBehaviour
{
    // Referenz auf den Pfeil
    public GameObject blinkingArrow;

    // Referenz auf den VideoPlayer,
    public VideoPlayer videoPlayer;

    // Referenz auf das ScreenFade-Script
    public ScreenFade screenFade;

    // Position, an die der Player während der Cutscene teleportiert wird
    public Transform spawnPoint;

    // Zielpunkt für die Kamera während der Cutscene
    public Transform cameraFocusTarget;

    // Zeitspanne, die die Kamera nach dem Fade-In
    // noch auf der Cutscene bleibt
    public float freezeAfterFadeTime = 5f;

    // Merker, damit die Cutscene nur EINMAL abgespielt wird
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Falls die Cutscene bereits abgespielt wurde -> abbrechen
        if (hasPlayed) return;

        // Nur der Player darf die Cutscene auslösen
        if (!other.CompareTag("Player")) return;

        // Markieren, dass die Cutscene jetzt gespielt wird
        hasPlayed = true;

        // Startet die komplette Cutscene-Abfolge als Coroutine
        StartCoroutine(PlayCutscene(other));
    }

    private IEnumerator PlayCutscene(Collider2D playerCollider)
    {
        // BlinkingArrow ausblenden, sobald die Cutscene startet
        if (blinkingArrow != null)
            blinkingArrow.SetActive(false);

        // Referenzen auf typische Player-Komponenten holen
        PlayerMovement movement = playerCollider.GetComponent<PlayerMovement>();
        Rigidbody2D rb = playerCollider.GetComponent<Rigidbody2D>();
        Animator animator = playerCollider.GetComponent<Animator>();

        // Referenz auf das CameraFollow-Script der MainCamera holen
        CameraFollow camFollow = Camera.main != null
            ? Camera.main.GetComponent<CameraFollow>()
            : null;

        // =========================
        // PLAYER EINFRIEREN
        // =========================

        // Bewegungsscript deaktivieren
        if (movement != null) movement.enabled = false;

        // Physik stoppen (keine Restbewegung)
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // Animator deaktivieren (keine Lauf-/Idle-Animationen)
        if (animator != null) animator.enabled = false;

        // =========================
        // FADE TO BLACK
        // =========================

        // Bildschirm ausblenden und warten, bis der Fade fertig ist
        if (screenFade != null)
            yield return screenFade.FadeOut();

        // =========================
        // KAMERA UMSCHALTEN
        // =========================

        // Kamera vom Player lösen und auf das Cutscene-Ziel setzen
        if (camFollow != null && cameraFocusTarget != null)
            camFollow.SetTarget(cameraFocusTarget);

        // =========================
        // PLAYER TELEPORTIEREN
        // =========================

        // Player während schwarzem Bildschirm versetzen,
        // damit der Übergang unsichtbar bleibt
        if (spawnPoint != null)
            playerCollider.transform.position = spawnPoint.position;

        // =========================
        // ANDERE VIDEOS STOPPEN
        // =========================

        // Sicherheit: Alle anderen VideoPlayer stoppen,
        // damit nicht mehrere Videos gleichzeitig laufen
        if (videoPlayer != null)
        {
            VideoPlayer[] allPlayers = FindObjectsOfType<VideoPlayer>();
            for (int i = 0; i < allPlayers.Length; i++)
            {
                if (allPlayers[i] != null && allPlayers[i] != videoPlayer)
                    allPlayers[i].Stop();
            }
        }

        // =========================
        // CUTSCENE STARTEN
        // =========================

        // Das zugehörige Video abspielen
        if (videoPlayer != null)
            videoPlayer.Play();

        // =========================
        // FADE IN
        // =========================

        // Bildschirm wieder einblenden
        if (screenFade != null)
            yield return screenFade.FadeIn();

        // =========================
        // CUTSCENE-ZEIT
        // =========================

        // Kamera bleibt für eine gewisse Zeit
        // auf dem Cutscene-Fokuspunkt
        yield return new WaitForSeconds(freezeAfterFadeTime);

        // =========================
        // KAMERA ZURÜCK ZUM PLAYER
        // =========================

        // Kamera folgt wieder dem Player
        if (camFollow != null)
            camFollow.SetTarget(playerCollider.transform);

        // =========================
        // PLAYER WIEDER FREIGEBEN
        // =========================

        // Animator wieder aktivieren
        if (animator != null) animator.enabled = true;

        // Bewegung wieder aktivieren
        if (movement != null) movement.enabled = true;

        // =========================
        // TRIGGER DEAKTIVIEREN
        // =========================

        // Trigger-Collider ausschalten,
        // damit die Cutscene nicht erneut ausgelöst werden kann
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }
}
