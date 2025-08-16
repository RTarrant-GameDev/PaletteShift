using UnityEngine;
using TMPro;

public class MissionTimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI TimerText;

    private void Start()
    {
        if (MissionTimer.TimerInstance != null)
        {
            MissionTimer.TimerInstance.OnTimeUpdated += UpdateUI;
            MissionTimer.TimerInstance.OnTimerStarted += MissionTimer.TimerInstance.StartTimer;
            MissionTimer.TimerInstance.StartTimer();
        }
    }

    private void OnDisable()
    {
        if (MissionTimer.TimerInstance != null)
        {
            MissionTimer.TimerInstance.OnTimeUpdated -= UpdateUI;
        }
    }


    private void UpdateUI(float TimePassed)
    {
        if (TimerText == null) return; //prevent null reference

        int minutes = Mathf.FloorToInt(TimePassed / 60);
        int seconds = Mathf.FloorToInt(TimePassed % 60);

        TimerText.text = $"{minutes:00}:{seconds:00}";
    }
}
