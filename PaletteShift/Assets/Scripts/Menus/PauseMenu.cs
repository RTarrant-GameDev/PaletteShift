using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Update is called once per frame
    public void ResumeGame()
    {
        GameSystemManagerScript.GameSystemManagerInstance.RestartGame();
    }

    public void QuitGame() {
        GameSystemManagerScript.GameSystemManagerInstance.QuitToMainMenu();
    }
}
