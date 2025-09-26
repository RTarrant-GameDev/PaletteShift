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
    }

    private void Start()
    {
        // Subscribe to CanvasManager pause/resume events
        if (CanvasManager.CanvasManagerInstance != null)
        {
            CanvasManager.CanvasManagerInstance.Pause += HandlePause;
            CanvasManager.CanvasManagerInstance.Resume += HandleResume;
        }
    }

    private void OnDestroy()
    {
        if (CanvasManager.CanvasManagerInstance != null)
        {
            CanvasManager.CanvasManagerInstance.Pause -= HandlePause;
            CanvasManager.CanvasManagerInstance.Resume -= HandleResume;
        }
    }

    private void Update()
    {
        if (!TimerRunning) return;

        TimePassed += Time.deltaTime;  // respects Time.timeScale
        OnTimeUpdated?.Invoke(TimePassed);
        Debug.Log("Timer running");
    }

    // Start counting from 0
    public void StartTimer()
    {
        TimePassed = 0f;
        TimerRunning = true;
        OnTimerStarted?.Invoke();
        OnTimeUpdated?.Invoke(TimePassed); // update UI immediately
    }

    // Stop counting
    public void StopTimer()
    {
        TimerRunning = false;
        OnTimerStopped?.Invoke();
        OnTimeUpdated?.Invoke(TimePassed); // update UI immediately
    }

    // Reset without starting
    public void ResetTimer()
    {
        TimePassed = 0f;
        TimerRunning = false;
        OnTimerReset?.Invoke();
        OnTimeUpdated?.Invoke(TimePassed); // update UI immediately
    }

    private void HandlePause()
    {
        wasRunningBeforePause = TimerRunning;
        TimerRunning = false;
        OnTimeUpdated?.Invoke(TimePassed); // freeze UI at current time
    }

    private void HandleResume()
    {
        if (wasRunningBeforePause)
        {
            TimerRunning = true;
            OnTimeUpdated?.Invoke(TimePassed); // refresh UI immediately
        }
    }
}
