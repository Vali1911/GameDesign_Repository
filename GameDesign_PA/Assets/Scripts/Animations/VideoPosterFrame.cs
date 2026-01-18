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

        // Prepare loads the first frame
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    private void OnPrepared(VideoPlayer vp)
    {
        // Show the first frame, then pause it
        vp.Play();
        vp.Pause();
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.prepareCompleted -= OnPrepared;
    }
}
