using System;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    public static MissionTimer TimerInstance { get; private set; }

    public float TimePassed { get; private set; }
    public bool TimerRunning { get; private set; }

    public event Action<float> OnTimeUpdated; // passes current elapsed time
    public event Action OnTimerStarted;
    public event Action OnTimerStopped;
    public event Action OnTimerReset;

    private bool wasRunningBeforePause;

    private void Awake()
    {
        if (TimerInstance != null && TimerInstance != this)
        {
            Destroy(this);
            return;
        }

        TimerInstance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (!TimerRunning) return;

        TimePassed += Time.deltaTime;  // respects Time.timeScale
        OnTimeUpdated?.Invoke(TimePassed);
    }

    // Start counting from 0
    public void StartTimer()
    {
        TimePassed = 0f;
        TimerRunning = true;
        OnTimerStarted?.Invoke();
        OnTimeUpdated?.Invoke(TimePassed); // update UI immediately
    }

    public void ResumeTimer() {
        TimerRunning = true;
        OnTimerStarted?.Invoke();
    }

    // Stop counting
    public void StopTimer()
    {
        TimerRunning = false;
        OnTimerStopped?.Invoke();
    }

    // Reset without starting
    public void ResetTimer()
    {
        TimePassed = 0f;
        TimerRunning = false;
        OnTimerReset?.Invoke();
        OnTimeUpdated?.Invoke(TimePassed); // update UI immediately
    }
}
