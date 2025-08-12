using UnityEngine;

public class GameplayEffectHandler : MonoBehaviour
{

    private void OnEnable() {
        EventObserver.OnObstacleHit += HandleObstacleHit;
    }

    private void HandleObstacleHit(Color color, GameObject player, Collision2D collision) {
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

            case var _ when color == Color.orange:
                Vector2 dir = (
                    collision.contacts[0].point -
                    new Vector2(
                        GameObject.Find("PlayerCharacter").transform.position.x,
                        GameObject.Find("PlayerCharacter").transform.position.y
                    )
                );

                dir = -dir.normalized;

                GameObject.Find("PlayerCharacter").GetComponent<Rigidbody2D>().AddForce(dir*200);
                break;

            default:
                break;
        }
    }
}
