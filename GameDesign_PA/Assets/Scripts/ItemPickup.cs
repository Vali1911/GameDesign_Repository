using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Icon einsammeln
        if (collision.CompareTag("Player"))
        {
            collected = true;
            gameObject.SetActive(false); // Icon verschwindet
        }
    }
}
