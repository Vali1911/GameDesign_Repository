using UnityEngine;
using UnityEngine.Video;

public class VideoPosterFrame2 : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        videoPlayer.prepareCompleted -= OnPrepared;
        videoPlayer.prepareCompleted += OnPrepared;

        videoPlayer.Prepare();
    }

    private void OnPrepared(VideoPlayer vp)
    {
        // Show first frame once
        vp.Play();
        vp.Pause();

        // IMPORTANT: Unsubscribe so it cannot pause the video later
        vp.prepareCompleted -= OnPrepared;
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.prepareCompleted -= OnPrepared;
    }
}