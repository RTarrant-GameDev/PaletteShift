using System;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager CanvasManagerInstance { get; private set; }
    public GameObject PlayerHUD;
    public GameObject PauseMenu;
    public GameObject GameOverMenuObj;
    public GameObject LevelCompleteMenuObj;

    [SerializeField]
    private bool PauseMenuActive;

    public event Action Pause;
    public event Action Resume;

    private void Awake() {
        if (CanvasManagerInstance != null && CanvasManagerInstance != this)
        { //if there is already an instance, destroy this instance
            Destroy(this);
            return;
        }

        CanvasManagerInstance = this;
    }

    void Start() {
        StartGame();
    }

    // make it so that, when level starts, only HUD is visible to player
    public void StartGame() {
        PlayerHUD.SetActive(true);
        PauseMenu.SetActive(false);
        GameOverMenuObj.SetActive(false);
        LevelCompleteMenuObj.SetActive(false);

        SetTimeScale(1.0f);
    }

    public void PauseGame()
    {
        PlayerHUD.SetActive(false);
        PauseMenu.SetActive(true);

        SetTimeScale(0.0f);
        Pause?.Invoke(); // fire pause event
    }

    public void ResumeGame()
    {
        PlayerHUD.SetActive(true);
        PauseMenu.SetActive(false);

        SetTimeScale(1.0f);
        Resume?.Invoke(); // fire resume event
    }

    public void LevelComplete(float Health, float Time, float Score) {
        LevelCompleteMenuObj.GetComponent<LevelCompleteMenu>().Health = Health;
        LevelCompleteMenuObj.GetComponent<LevelCompleteMenu>().Time = Time;
        LevelCompleteMenuObj.GetComponent<LevelCompleteMenu>().Score = Score;

        LevelCompleteMenuObj.SetActive(true);
        PlayerHUD.SetActive(false);
        SetTimeScale(0.0f);
    }

    public void GameOver() {
        GameOverMenuObj.SetActive(true);
        PlayerHUD.SetActive(false);
        SetTimeScale(0.0f);
    }

    public void SetTimeScale(float TimeScale){
        Time.timeScale = TimeScale;
    }
}
