using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartFunction() {
        SceneManager.LoadScene("GameplayScene");
        GameSystemManagerScript.GameSystemManagerInstance.ChangeState(GameState.Gameplay);
        EventObserver.TriggerEvent("GenerateNextLevel");
    }

    public void TutorialFunction() {
        SceneManager.LoadScene("TutorialScene");
        GameSystemManagerScript.GameSystemManagerInstance.ChangeState(GameState.Gameplay);
        EventObserver.TriggerEvent("GenerateTutorialLevel");
    }

    public void LevelSelectFunction() {
        CanvasManager.CanvasManagerInstance.ShowLevelSelectMenu();
    }

    public void QuitFunction() {
        Application.Quit();
    }
}
