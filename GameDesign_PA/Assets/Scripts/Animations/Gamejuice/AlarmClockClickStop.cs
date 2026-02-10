using UnityEngine;

public class AlarmClockClickStop : MonoBehaviour
{
    public AlarmClockAutoRing ringScript;

    private void OnMouseDown()
    {
        if (ringScript != null)
        {
            ringScript.StopRingingExternally();
        }
    }
}
