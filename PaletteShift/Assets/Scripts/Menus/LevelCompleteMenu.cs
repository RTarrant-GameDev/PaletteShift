using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour {

    public float Health;
    public float Time;
    public float Score;

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI GradeText;
    public Button NextLevelButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() {
        if (LevelManager.LevelManagerInstance.Levels.Last() != LevelManager.LevelManagerInstance.CurrentLevel) {
            NextLevelButton.gameObject.SetActive(true);
        } else {
            NextLevelButton.gameObject.SetActive(false);
        }

        HealthText.SetText($"Health: {Health}");

        int minutes = Mathf.FloorToInt(Time / 60);
        int seconds = Mathf.FloorToInt(Time % 60);

        TimeText.SetText($"Time: {minutes:00}:{seconds:00}");
        
        string LetterGrade;
        
        if(Score >= 80.0f) {
            LetterGrade = "S";
        } else if (Score >= 75.0f && Score < 80.0f) {
            LetterGrade = "A+";
        } else if (Score >= 70.0f && Score < 75.0f) {
            LetterGrade = "A";
        } else if (Score >= 60.0f && Score < 70.0f) {
            LetterGrade = "B";
        } else if (Score >= 50.0f && Score < 60.0f) {
            LetterGrade = "C";
        } else {
            LetterGrade = "D";
        }

        GradeText.SetText($"Grade: {LetterGrade}");
    }

    public void NextLevel() {
        GameSystemManagerScript.GameSystemManagerInstance.NextLevel();
    }

    public void RestartFunction(){
        GameSystemManagerScript.GameSystemManagerInstance.RestartGame();
    }

    public void QuitFunction() {
        GameSystemManagerScript.GameSystemManagerInstance.QuitToMainMenu();
    }
}
