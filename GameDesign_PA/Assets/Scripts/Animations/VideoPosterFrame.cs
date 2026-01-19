using UnityEngine;
using UnityEngine.Video;

public class VideoPosterFrame : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        // Important: do NOT let it start playing by itself
        videoPlayer.Stop();

        // Prepare so the texture can get a frame when needed
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    private void OnPrepared(VideoPlayer vp)
    {
        // Show first frame without "playing" the whole scene:
        // Step to frame 0 and pause.
        // Some Unity versions only update texture after a Play/Pause,
        // so we do a very short "Play then Pause" ONLY for this player.
        vp.Play();
        vp.Pause();
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.prepareCompleted -= OnPrepared;
    }
}
