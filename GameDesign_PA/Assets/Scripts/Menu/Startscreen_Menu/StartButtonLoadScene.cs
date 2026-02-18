using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButtonLoadScene : MonoBehaviour, IPointerUpHandler
{
    [Header("Scene to load")]
    public string sceneName = "SampleScene";

    public void OnPointerUp(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneName);
    }
}