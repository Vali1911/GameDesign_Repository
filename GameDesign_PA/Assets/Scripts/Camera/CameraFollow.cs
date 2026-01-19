using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Ziel der Kamera beim Spielbeginn
    public Transform startTarget;

    // Kamerageschwindigkeit
    public float followSpeed = 5f;

    // Offset der Kamera
    public Vector3 offset;

    // Aktuelles Ziel der Kamera
    private Transform currentTarget;

    void Start()
    {
        // Setzt das Anfangsziel der Kamera
        currentTarget = startTarget;
    }

    void LateUpdate()
    {
        // Falls kein Ziel gesetzt ist, nichts tun
        if (currentTarget == null)
            return;

        // Zielposition berechnen:
        // Position des Ziels + Offset
        Vector3 targetPosition = currentTarget.position + offset;

        // Kamera sanft in Richtung Ziel bewegen
        // Lerp sorgt für eine weiche, nicht ruckelige Bewegung
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    // Öffentliche Methode, um das Kamera-Ziel zu wechseln
    public void SetTarget(Transform newTarget)
    {
        // Neues Ziel setzen (z. B. Cutscene-Fokuspunkt)
        currentTarget = newTarget;
    }
}
