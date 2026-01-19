using UnityEngine;
using UnityEngine.Video;

public class VideoPosterFrame : MonoBehaviour
{
    // Referenz auf den VideoPlayer,
    public VideoPlayer videoPlayer;

    void Awake()
    {
        // Falls im Inspector kein VideoPlayer zugewiesen wurde,
        // holen wir uns automatisch den VideoPlayer
        // vom selben GameObject
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
            
        // Verhindert, dass das Video automatisch beim Aktivieren startet
        videoPlayer.playOnAwake = false;

        // Sorgt dafür, dass Unity auf den ersten Frame wartet,
        // bevor etwas gerendert wird
        // (verhindert schwarzes oder leeres Bild)
        videoPlayer.waitForFirstFrame = true;

        // Sicherheit: explizit stoppen,
        // damit das Video garantiert nicht abspielt
        videoPlayer.Stop();

        // Das Video vorbereiten und ersten Frame laden
        videoPlayer.Prepare();

        // Event abonnieren, das ausgelöst wird,
        // sobald das Video vollständig vorbereitet ist
        videoPlayer.prepareCompleted += OnPrepared;
    }

    private void OnPrepared(VideoPlayer vp)
    {
        // Ziel: Das ERSTE Frame anzeigen,

        // Kurz abspielen
        vp.Play();

        // Und gleich pausieren
        vp.Pause();
    }

    void OnDestroy()
    {
        // Event wieder abmelden, wenn das Objekt zerstört wird
        // (verhindert Memory Leaks / Fehler)
        if (videoPlayer != null)
            videoPlayer.prepareCompleted -= OnPrepared;
    }
}
