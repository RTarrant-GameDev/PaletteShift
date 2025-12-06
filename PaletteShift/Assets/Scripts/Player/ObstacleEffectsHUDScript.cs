using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleEffectsHUDScript : MonoBehaviour {
    public TextMeshProUGUI ObstacleEffectText;

    void Start()
    {
        SetUIElementVisibility(false); //have UI elements hidden at start
    }

    public void DisplayEffectText(string TextToChange, Color TextColor)
    {
        StartCoroutine(ColorChangeUIUpdate(TextToChange, TextColor));
    }

    IEnumerator ColorChangeUIUpdate(string TextToChange, Color TextColor)
    {
        SetUIElementVisibility(true);
        ObstacleEffectText.color = TextColor;

        float timeLeft = 7.5f;

        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime; // Reduce time by deltaTime each frame

            // If you want to display the countdown
            if (ObstacleEffectText != null)
                ObstacleEffectText.text = TextToChange;

            yield return null; // Wait until next frame
        }

        // After timer ends, reset or do something
        if (ObstacleEffectText != null)
            ObstacleEffectText.text = "";

        SetUIElementVisibility(false);
    }

    // Created dedicated function for changing UI visibility to allow for better organisation
    void SetUIElementVisibility(bool ShouldBeActive) {
        ObstacleEffectText.enabled = ShouldBeActive;
    }

    public void ResetEffect() {
        SetUIElementVisibility(false);
    }
}
