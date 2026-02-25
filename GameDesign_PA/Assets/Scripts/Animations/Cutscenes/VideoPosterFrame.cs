using UnityEngine;
using UnityEngine.Video;

public class VideoPosterFrame : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        StartCoroutine(PrepareAndShow());
    }

    private System.Collections.IEnumerator PrepareAndShow()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
        yield return null; // 1 Frame warten
        videoPlayer.Pause();
    }
}