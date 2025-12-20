using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Video;

public enum GameState
{
    Menu,
    Gameplay,
    Paused,
    GameOver,
    LevelComplete
}

public class GameSystemManagerScript : MonoBehaviour {

    public static GameSystemManagerScript GameSystemManagerInstance { get; private set; }
    public GameState CurrentState { get; private set; }
    public GameState PreviousState { get; private set; }

    public event Action<GameState> OnStateChanged;

    private float cachedFinalHealth, cachedMissionTime, cachedScore;

    private void Awake() {
        if (GameSystemManagerInstance != null && GameSystemManagerInstance != this) { //if there is already an instance, destroy this instance
            DestroyImmediate(this.gameObject);
            return;
        }

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        GameSystemManagerInstance = this;
        DontDestroyOnLoad(GameSystemManagerInstance);
    }

    // Kick off essential game systems on Start (StartTimer, etc)
    void Start() {
        ChangeState(GameState.Menu);
    }

    public void ChangeState(GameState NewState) {
        ExitState(CurrentState);
        PreviousState = CurrentState;
        CurrentState = NewState;
        EnterState(NewState);

        OnStateChanged?.Invoke(NewState);
    }

#region "Enter/Exit State Logic"

    private void EnterState(GameState stateToEnter) {
        switch (stateToEnter) {
            case GameState.Menu:
                CanvasManager.CanvasManagerInstance.AssignCamera();
                Time.timeScale = 1f;
                MissionTimer.TimerInstance.StopTimer();
                MissionTimer.TimerInstance.ResetTimer();
                CanvasManager.CanvasManagerInstance.ShowMenu();
                break;

            case GameState.Gameplay:
                CanvasManager.CanvasManagerInstance.AssignCamera();
                Time.timeScale = 1f;
                CanvasManager.CanvasManagerInstance.ShowGameplayHUD();
                if (PreviousState == GameState.Paused) 
                    MissionTimer.TimerInstance.ResumeTimer();
                else 
                    MissionTimer.TimerInstance.StartTimer();
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                CanvasManager.CanvasManagerInstance.ShowPauseMenu();
                MissionTimer.TimerInstance.StopTimer();
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                CanvasManager.CanvasManagerInstance.ShowGameOverMenu();
                MissionTimer.TimerInstance.StopTimer();
                break;

            case GameState.LevelComplete:
                Time.timeScale = 0f;
                CanvasManager.CanvasManagerInstance.ShowLevelCompleteMenu(cachedFinalHealth, cachedMissionTime, cachedScore);
                MissionTimer.TimerInstance.StopTimer();
                break;
        }
    }

    private void ExitState(GameState stateToExit) {
        switch(stateToExit) {
            case GameState.Menu:
                CanvasManager.CanvasManagerInstance.HideMenu();
                break;

            case GameState.Gameplay:
                CanvasManager.CanvasManagerInstance.HideGameplayHUD();
                break;

            case GameState.Paused:
                CanvasManager.CanvasManagerInstance.HidePauseMenu();
                break;

            case GameState.GameOver:
                CanvasManager.CanvasManagerInstance.HideGameOverMenu();
                break;

            case GameState.LevelComplete:
                CanvasManager.CanvasManagerInstance.HideLevelCompleteMenu();
                break;
        }   
    }

#endregion

#region "UI Button events"

    public void PauseGame() {
        ChangeState(GameState.Paused);
    }

    public void ResumeGame() {
        ChangeState(GameState.Gameplay);
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ColorChangeManager.ManagerInstance.StopAllCoroutines();
        ColorChangeManager.ManagerInstance.ResetColor();
        EventObserver.TriggerEvent("DestroyCurrentLevel");
        EventObserver.TriggerEvent("GenerateNextLevel");
        ChangeState(GameState.Gameplay);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ColorChangeManager.ManagerInstance.StopAllCoroutines();
        ColorChangeManager.ManagerInstance.ResetColor();
        EventObserver.TriggerEvent("DestroyCurrentLevel");
        EventObserver.TriggerEvent("GenerateCurrentLevel");
        ChangeState(GameState.Gameplay);
    }

    public void QuitToMainMenu() {
        EventObserver.TriggerEvent("DestroyCurrentLevel");
        ColorChangeManager.ManagerInstance.StopAllCoroutines();
        ColorChangeManager.ManagerInstance.ResetColor();
        SceneManager.LoadScene("MainMenuScene");
        ChangeState(GameState.Menu);
    }

#endregion

    public void LevelCompleted(float Health, float Time, float Score) {
        cachedFinalHealth = Health;
        cachedMissionTime = Time;
        cachedScore = Score;
        EventObserver.TriggerEvent("SetLevelAsComplete");
        ChangeState(GameState.LevelComplete);
    }
}
