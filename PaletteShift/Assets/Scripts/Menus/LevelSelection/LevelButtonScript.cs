using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour {
    public Level LevelInButton;
    void OnEnable() {
        if (LevelInButton.LevelScreenshot != null) {
            GetComponentInChildren<Image>().sprite = LevelInButton.LevelScreenshot;
        }
        GetComponentInChildren<TextMeshProUGUI>().text = $"Level {LevelInButton.LevelNumber}";
    }

    public void LevelButtonClick() {
        GetComponentInParent<LevelSelectMenu>().SelectLevel(LevelInButton);
    }
}
