using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager CanvasManagerInstance { get; private set; }

    [Header("Canvas references")]
    public GameObject MainMenuCanvas;
    public GameObject GameplayHUD;
    public GameObject PauseMenuCanvas;
    public GameObject GameOverMenuCanvas;
    public GameObject LevelCompleteMenuCanvas;
    public GameObject TutorialTextObj;

    [SerializeField]
    private bool PauseMenuActive;

    private void Awake() {
        if (CanvasManagerInstance != null && CanvasManagerInstance != this) { //if there is already an instance, destroy this instance
            Destroy(this);
            return;
        }

        CanvasManagerInstance = this;
        DontDestroyOnLoad(this);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void HideAll() {
        MainMenuCanvas.SetActive(false);
        GameplayHUD.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        GameOverMenuCanvas.SetActive(false);
        LevelCompleteMenuCanvas.SetActive(false);
    }

    private void Start() {
        AssignCamera();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        AssignCamera();
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

    void ShowTutorialText(string TutorialText) {
        TutorialTextObj.SetActive(true);
        TutorialTextObj.GetComponentInChildren<TextMeshProUGUI>().SetText(TutorialText);
    }

    void HideTutorialText() {
        TutorialTextObj.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        TutorialTextObj.SetActive(false);
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

    private void AssignCamera() {
        // find new camera and assign to all UI
        Camera cam = Camera.main;

        MainMenuCanvas.GetComponent<Canvas>().worldCamera = cam;
        GameplayHUD.GetComponent<Canvas>().worldCamera = cam;
        PauseMenuCanvas.GetComponent<Canvas>().worldCamera = cam;
        GameOverMenuCanvas.GetComponent<Canvas>().worldCamera = cam;
        LevelCompleteMenuCanvas.GetComponent<Canvas>().worldCamera = cam;
    }

    public void DisplayTutorialText(string TutorialText) {
        StartCoroutine(TutorialTextDisplay(TutorialText));
    }

    IEnumerator TutorialTextDisplay(string TutorialText) {
        ShowTutorialText(TutorialText);
        yield return new WaitForSeconds(7.5f);
        HideTutorialText();
    }
}
