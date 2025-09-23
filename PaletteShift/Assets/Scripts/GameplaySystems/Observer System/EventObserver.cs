using System;
using UnityEngine;

public static class EventObserver
{
    public static event Action<Color, GameObject, GameObject> OnObstacleHit;

    public static void RaiseObstacleHit(Color ObstacleColor, GameObject PlayerObj, GameObject HitObstacle) {
        OnObstacleHit?.Invoke(ObstacleColor, PlayerObj, HitObstacle);
    }
}
