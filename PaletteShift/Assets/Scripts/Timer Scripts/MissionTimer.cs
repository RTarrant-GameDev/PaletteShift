using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    public static MissionTimer TimerInstance { get; private set; }

    public float TimePassed { get; private set; }
    public bool TimerRunning { get; private set; }

    public event Action<float> OnTimeUpdated; //passes current elapsed time;
    public event Action OnTimerStarted;
    public event Action OnTimerStopped;
    public event Action OnTimerReset;

    private void Awake()
    {
        if (TimerInstance != null && TimerInstance != this)
        { //if there is already an instance, destroy this instance
            Destroy(this);
            return;
        }

        TimerInstance = this; //assuming that there is no pre-existing TimerInstance, make this the current TimerInstance
    }

    private void Update()
    {
        if (!TimerRunning) return;

        TimePassed += Time.deltaTime;
        OnTimeUpdated?.Invoke(TimePassed);
    }

    public void StartTimer()
    {
        TimePassed = 0.0f;
        TimerRunning = true;
    }

    public void StopTimer()
    {
        TimerRunning = false;
        OnTimerStopped?.Invoke();
    }
    
    public void ResetTimer()
    {
        TimePassed = 0f;
        OnTimerReset?.Invoke();
    }
}
