using UnityEngine;
using TMPro;

public class MissionTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;

    private void Start()
    {
        if (MissionTimer.TimerInstance != null)
        {
            MissionTimer.TimerInstance.OnTimeUpdated += UpdateUI;
            MissionTimer.TimerInstance.OnTimerStarted += UpdateUIOnStart;
            MissionTimer.TimerInstance.OnTimerStopped += UpdateUIOnStop;
            MissionTimer.TimerInstance.OnTimerReset += UpdateUIOnReset;
        }
    }

    private void OnDestroy()
    {
        if (MissionTimer.TimerInstance != null)
        {
            MissionTimer.TimerInstance.OnTimeUpdated -= UpdateUI;
            MissionTimer.TimerInstance.OnTimerStarted -= UpdateUIOnStart;
            MissionTimer.TimerInstance.OnTimerStopped -= UpdateUIOnStop;
            MissionTimer.TimerInstance.OnTimerReset -= UpdateUIOnReset;
        }
    }

    private void UpdateUI(float timePassed)
    {
        if (TimerText == null) return;

        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);

        TimerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateUIOnStart()
    {
        UpdateUI(MissionTimer.TimerInstance.TimePassed);
    }

    private void UpdateUIOnStop()
    {
        UpdateUI(MissionTimer.TimerInstance.TimePassed);
    }

    private void UpdateUIOnReset()
    {
        UpdateUI(MissionTimer.TimerInstance.TimePassed);
    }
}