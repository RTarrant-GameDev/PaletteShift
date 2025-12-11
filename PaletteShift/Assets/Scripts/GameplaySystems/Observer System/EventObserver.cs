using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventObserver : MonoBehaviour {
    private Dictionary<string, UnityEvent> eventDictionary;
    public static event Action<Color, GameObject, GameObject> OnObstacleHit;

    public static EventObserver EventObserverInstance { get; private set; }

    public static void RaiseObstacleHit(Color ObstacleColor, GameObject PlayerObj, GameObject HitObstacle) {
        OnObstacleHit?.Invoke(ObstacleColor, PlayerObj, HitObstacle);
    }

    private void Awake() {
        if (EventObserverInstance != null && EventObserverInstance != this) { //if there is already an instance, destroy this instance
            Destroy(this);
            return;
        }

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        EventObserverInstance = this;
        EventObserverInstance.Init();
        DontDestroyOnLoad(gameObject);
    }

    void Init() {
        if(eventDictionary == null) {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener) {
        UnityEvent thisEvent = null;
        if(EventObserverInstance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            EventObserverInstance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener) {
        if(EventObserverInstance == null) return;
        UnityEvent thisEvent = null;
        if(EventObserverInstance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName) {
        UnityEvent thisEvent = null;
        if(EventObserverInstance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
    }
}
