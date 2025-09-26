using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystemManagerScript : MonoBehaviour {

    public static GameSystemManagerScript GameSystemManagerInstance { get; private set; }

    private void Awake() {
        if (GameSystemManagerInstance != null && GameSystemManagerInstance != this)
        { //if there is already an instance, destroy this instance
            Destroy(this);
            return;
        }

        GameSystemManagerInstance = this;
    }

    // Kick off essential game systems on Start (StartTimer, etc)
    void Start() {
        CanvasManager.CanvasManagerInstance.StartGame();
        MissionTimer.TimerInstance.StartTimer();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CanvasManager.CanvasManagerInstance.StartGame();
        ColorChangeManager.ManagerInstance.ResetColor();
        MissionTimer.TimerInstance.StartTimer();
    }

    public void QuitToMainMenu() {
        MissionTimer.TimerInstance.StopTimer();
        SceneManager.LoadScene("MainMenuScene");
    }
}
