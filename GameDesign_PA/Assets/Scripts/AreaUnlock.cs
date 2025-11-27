using UnityEngine;

public class AreaUnlock : MonoBehaviour
{
    public GameObject barrier;
    public ItemPickup item;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item.collected)
        {
            barrier.SetActive(false); // Barriere verschwindet
        }
    }
}
