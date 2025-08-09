using System;
using UnityEngine;

public static class EventObserver
{
    public static event Action<Color, GameObject> OnObstacleHit;

    public static void RaiseObstacleHit(Color color, GameObject player) {
        OnObstacleHit?.Invoke(color, player);
    }
}
