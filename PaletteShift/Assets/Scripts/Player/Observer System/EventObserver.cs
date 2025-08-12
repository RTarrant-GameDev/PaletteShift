using System;
using UnityEngine;

public static class EventObserver
{
    public static event Action<Color, GameObject, Collision2D> OnObstacleHit;

    public static void RaiseObstacleHit(Color color, GameObject player, Collision2D collision) {
        OnObstacleHit?.Invoke(color, player, collision);
    }
}
