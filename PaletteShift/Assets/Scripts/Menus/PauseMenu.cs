using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Update is called once per frame
    public void ResumeGame()
    {
        CanvasManager.CanvasManagerInstance.ResumeGame();
    }

    public void QuitGame() {
        GameSystemManagerScript.GameSystemManagerInstance.QuitToMainMenu();
    }
}
