using UnityEngine;
using UnityEngine.Video;
using System;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public void Play(Action onFinished)
    {
        Debug.Log("CutsceneController.Play() wurde aufgerufen");


        videoPlayer.Stop();

        // Event ZUERST registrieren
        videoPlayer.loopPointReached += OnVideoFinished;

        videoPlayer.Play();

        void OnVideoFinished(VideoPlayer vp)
        {
            Debug.Log("Cutscene beendet");
            videoPlayer.loopPointReached -= OnVideoFinished;
            onFinished?.Invoke();
        }
    }
}