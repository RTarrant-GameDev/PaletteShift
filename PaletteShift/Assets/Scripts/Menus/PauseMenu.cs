using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Update is called once per frame
    public void ResumeGame()
    {
        CanvasManager.CanvasManagerInstance.ResumeGame();
    }
}
