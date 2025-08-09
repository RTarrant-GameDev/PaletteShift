using UnityEngine;

public class GameplayEffectHandler : MonoBehaviour
{

    private void OnEnable() {
        EventObserver.OnObstacleHit += HandleObstacleHit;
    }

    private void HandleObstacleHit(Color color, GameObject player) {
        switch (color) {
            case var _ when color == Color.blue:
                Debug.Log("test");
                break;

            default:
                break;
        }
    }
}
