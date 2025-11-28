using UnityEngine;

public class FolderClick : MonoBehaviour
{
    public PlayerInteraction interactionScript;
    private bool taken = false;

    public void OnFolderClicked()
    {
        if (taken) return;
        taken = true;

        if (interactionScript != null)
            interactionScript.folderTaken = true;

        gameObject.SetActive(false);
    }
}
