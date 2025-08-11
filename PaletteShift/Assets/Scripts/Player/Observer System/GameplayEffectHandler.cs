using UnityEngine;

public class GameplayEffectHandler : MonoBehaviour
{

    private void OnEnable() {
        EventObserver.OnObstacleHit += HandleObstacleHit;
    }

    private void HandleObstacleHit(Color color, GameObject player) {
        switch (color) {
            case var _ when color == Color.blue:
                GameObject.Find("PlayerCharacter").GetComponent<HealthManagementScript>().DamagePlayer(1);
                break;

            case var _ when color == Color.red:
                Debug.Log("Player dead");
                break;

            case var _ when color == Color.green:
                GameObject.Find("PlayerCharacter").GetComponent<PlayerMovement>().TriggerMoveSpeedChange(1.25f);
                break;

            case var _ when color == Color.yellow:
                GameObject.Find("PlayerCharacter").GetComponent<PlayerMovement>().TriggerControlReverse();
                break;

            default:
                break;
        }
    }
}
