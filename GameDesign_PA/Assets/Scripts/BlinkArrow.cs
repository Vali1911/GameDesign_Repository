using UnityEngine;
using UnityEngine.UI;

public class BlinkArrow : MonoBehaviour
{
    public Image arrowImage;
    public float speed = 2f;

    private bool isBlinking = false;

    private void Update()
    {
        if (isBlinking)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * speed));
            arrowImage.color = new Color(arrowImage.color.r, arrowImage.color.g, arrowImage.color.b, alpha);
        }
    }

    // Pfeil anzeigen
    public void ShowArrow()
    {
        isBlinking = true;
        arrowImage.gameObject.SetActive(true);
    }

    // Pfeil ausblenden
    public void HideArrow()
    {
        isBlinking = false;
        arrowImage.gameObject.SetActive(false);
    }
}
