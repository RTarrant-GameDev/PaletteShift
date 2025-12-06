using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeUIScript : MonoBehaviour {
    public TextMeshProUGUI ColorTimerText;
    public Image ColorTargetImage;

    void OnEnable()
    {
        Debug.Log("UI active");
        if (ColorChangeManager.ManagerInstance != null)
        {
            Debug.Log("Color change manager found");
            ColorChangeManager.ManagerInstance.OnColorChange += HandleColorChange;
        }

        SetUIElementVisibility(false); //have UI elements hidden at start
    }

    private void HandleColorChange(Color colorToChange, float colorChangeTime)
    {
        Debug.Log("Colour change UI active");
        StartCoroutine(ColorChangeUIUpdate(colorToChange, colorChangeTime));
    }

    IEnumerator ColorChangeUIUpdate(Color ColorToChange, float ColorChangeTime)
    {

        SetUIElementVisibility(true);

        float timeLeft = ColorChangeTime;

        // Optionally, set the color once at the beginning
        if (ColorTargetImage != null)
            ColorTargetImage.color = ColorToChange;

        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime; // Reduce time by deltaTime each frame

            // If you want to display the countdown
            if (ColorTimerText != null)
                ColorTimerText.text = Mathf.Ceil(timeLeft).ToString();

            yield return null; // Wait until next frame
        }

        // After timer ends, reset or do something
        if (ColorTimerText != null)
            ColorTimerText.text = "0";

        SetUIElementVisibility(false);
    }

    // Created dedicated function for changing UI visibility to allow for better organisation
    void SetUIElementVisibility(bool ShouldBeActive) {
        ColorTargetImage.enabled = ShouldBeActive;
        ColorTimerText.enabled = ShouldBeActive;
    }
}
