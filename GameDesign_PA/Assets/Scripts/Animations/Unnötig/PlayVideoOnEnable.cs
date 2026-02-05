/*
 * using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnEnable : MonoBehaviour
{
    // Referenz auf den VideoPlayer,
    private VideoPlayer vp;

    // Awake wird einmal aufgerufen, wenn das GameObject geladen wird
    void Awake()
    {
        // Videoplayer einmal zu Beginn holen
        vp = GetComponent<VideoPlayer>();
    }

    // OnEnable wird jedes Mal aufgerufen wenn das GameObject aktiviert wird
    void OnEnable()
    {
        // Abspielzeit wird auf Anfang zurückgesetzt
        vp.time = 0;

        // Startet die Wiedergabe des Videos
        // -> Panel wird aktiv = Cutscene startet automatisch
        vp.Play();
    }
}
*/
