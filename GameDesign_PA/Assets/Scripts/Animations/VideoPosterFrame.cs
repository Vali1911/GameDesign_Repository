using UnityEngine;
using UnityEngine.Video;

public class VideoPosterFrame : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Awake()
    {
        // Falls nichts im Inspector gesetzt ist
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        // Video vorbereiten
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    private void OnPrepared(VideoPlayer vp)
    {
        // Erstes Frame anzeigen
        vp.Play();
        vp.Pause();
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.prepareCompleted -= OnPrepared;
    }
}