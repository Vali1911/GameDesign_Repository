using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform startTarget;      // Player beim Spielstart
    public float followSpeed = 5f;
    public Vector3 offset;

    private Transform currentTarget;

    void Start()
    {
        // Kamera folgt beim Spielstart dem Player
        currentTarget = startTarget;
    }

    void LateUpdate()
    {
        if (currentTarget == null)
            return;

        Vector3 targetPosition = currentTarget.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    // Wird von Panels / Cutscenes aufgerufen
    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}