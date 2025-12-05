using System;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager CanvasManagerInstance { get; private set; }

    [Header("Canvas references")]
    public GameObject MainMenuCanvas;
    public GameObject GameplayHUD;
    public GameObject PauseMenuCanvas;
    public GameObject GameOverMenuCanvas;
    public GameObject LevelCompleteMenuCanvas;

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
        DontDestroyOnLoad(this);
    }

    private void HideAll()
    {
        MainMenuCanvas.SetActive(false);
        GameplayHUD.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        GameOverMenuCanvas.SetActive(false);
        LevelCompleteMenuCanvas.SetActive(false);
    }


    public void ShowMenu() {
        HideAll();
        MainMenuCanvas.SetActive(true);
    }

    public void HideMenu() { 
        MainMenuCanvas.SetActive(false);
    }

    public void ShowGameplayHUD() {
        HideAll();
        GameplayHUD.SetActive(true);
    }

    public void HideGameplayHUD() { 
        GameplayHUD.SetActive(false);
    }

    public void ShowPauseMenu() {
        HideAll();
        PauseMenuCanvas.SetActive(true);
    }

    public void HidePauseMenu() { 
        PauseMenuCanvas.SetActive(false);
    }

    public void ShowGameOverMenu() {
        HideAll();
        GameOverMenuCanvas.SetActive(true);
    }

    public void HideGameOverMenu() { 
        GameOverMenuCanvas.SetActive(false);
    }

    public void ShowLevelCompleteMenu(float PlayerHealth, float MissionTime, float MissionScore) {
        HideAll();

        LevelCompleteMenuCanvas.GetComponent<LevelCompleteMenu>().Health = PlayerHealth;
        LevelCompleteMenuCanvas.GetComponent<LevelCompleteMenu>().Time = MissionTime;
        LevelCompleteMenuCanvas.GetComponent<LevelCompleteMenu>().Score = MissionScore;

        LevelCompleteMenuCanvas.SetActive(true);
    }

    public void HideLevelCompleteMenu() { 
        LevelCompleteMenuCanvas.SetActive(false);
    }
}
