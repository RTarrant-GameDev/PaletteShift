using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventObserver : MonoBehaviour
{
    private static EventObserver instance;
    private Dictionary<string, UnityEvent> eventDictionary;

    public static event Action<Color, GameObject, GameObject> OnObstacleHit;

    public static void RaiseObstacleHit(Color color, GameObject player, GameObject obstacle)
    {
        OnObstacleHit?.Invoke(color, player, obstacle);
    }

    private static EventObserver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventObserver>();

                if (instance == null)
                {
                    GameObject go = new GameObject("EventObserver");
                    instance = go.AddComponent<EventObserver>();
                }

                instance.Initialize();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Initialize();
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize()
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<string, UnityEvent>();
    }

    // ---------------- EVENTS ----------------

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (!Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent = new UnityEvent();
            Instance.eventDictionary[eventName] = thisEvent;
        }

        thisEvent.AddListener(listener);
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (instance == null) return;

        if (instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName)
    {
        if (instance == null) return;

        if (instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.Invoke();
    }
}