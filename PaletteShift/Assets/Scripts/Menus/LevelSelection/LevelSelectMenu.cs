using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable() {
        EventObserver.StartListening("ReturnToMainMenu", ReturnToMainMenu);
    }

    public void SelectLevel(Level ChosenLevel) {
        SceneManager.LoadScene("DemoScene");
        CanvasManager.CanvasManagerInstance.HideLevelSelectMenu();
        GameSystemManagerScript.GameSystemManagerInstance.ChangeState(GameState.Gameplay);
        LevelManager.SetSelectedLevel(ChosenLevel);
        EventObserver.TriggerEvent("GenerateLevel");
    }

    public void ReturnToMainMenu() {
        CanvasManager.CanvasManagerInstance.HideLevelSelectMenu();
        CanvasManager.CanvasManagerInstance.ShowMenu();
    }
}
