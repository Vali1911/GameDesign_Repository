using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnEnable : MonoBehaviour
{
    private VideoPlayer vp;

    void Awake()
    {
        vp = GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        vp.time = 0;
        vp.Play();
    }
}
