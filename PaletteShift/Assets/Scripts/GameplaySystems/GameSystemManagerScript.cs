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
            Destroy(this);
            return;
        }

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        GameSystemManagerInstance = this;
        DontDestroyOnLoad(gameObject);
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
                Time.timeScale = 1f;
                MissionTimer.TimerInstance.ResetTimer();
                CanvasManager.CanvasManagerInstance.ShowMenu();
                break;

            case GameState.Gameplay:
                Time.timeScale = 1f;
                CanvasManager.CanvasManagerInstance.ShowGameplayHUD();

                if(PreviousState == GameState.Menu)
                    MissionTimer.TimerInstance.StartTimer();
                else if (PreviousState == GameState.Paused) 
                    MissionTimer.TimerInstance.ResumeTimer();

                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                CanvasManager.CanvasManagerInstance.ShowPauseMenu();
                MissionTimer.TimerInstance.StopTimer();
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                ColorChangeManager.ManagerInstance.ResetColor();
                CanvasManager.CanvasManagerInstance.ShowGameOverMenu();
                MissionTimer.TimerInstance.StopTimer();
                break;

            case GameState.LevelComplete:
                Time.timeScale = 0f;
                ColorChangeManager.ManagerInstance.ResetColor();
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

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ColorChangeManager.ManagerInstance.ResetColor();
        ChangeState(GameState.Gameplay);
        MissionTimer.TimerInstance.StartTimer();
    }

    public void QuitToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
        ChangeState(GameState.Menu);
        MissionTimer.TimerInstance.StopTimer();
    }

#endregion

    public void LevelCompleted(float Health, float Time, float Score) {
        cachedFinalHealth = Health;
        cachedMissionTime = Time;
        cachedScore = Score;
        ChangeState(GameState.LevelComplete);
    }
}
