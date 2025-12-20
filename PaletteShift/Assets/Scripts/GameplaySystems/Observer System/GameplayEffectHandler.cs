using System.Collections;
using UnityEngine;

public class GameplayEffectHandler : MonoBehaviour
{
    public static GameplayEffectHandler GameplayEffectHandlerInstance {get; private set;}

    private void Awake() {
        if(GameplayEffectHandlerInstance != null && GameplayEffectHandlerInstance != this) {
            Destroy(this);
            return;
        }

        GameplayEffectHandlerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        EventObserver.OnObstacleHit += HandleObstacleHit;
    }

    private void HandleObstacleHit(Color color, GameObject player, GameObject obstacle) {
        bool ColorMatchFound = player.GetComponent<ColorMatchMechanic>().CompareColorWithObstacle(color);
        switch (color) {
            case var _ when color == Color.blue:
                if (ColorMatchFound != true) {
                    player.GetComponent<HealthManagementScript>().DamagePlayer(1);
                    AudioManager.AudioManagerInstance.PlaySFX(obstacle.GetComponent<Obstacle>().ObstacleSFX);
                    break;
                }
                break;

            case var _ when color == Color.red:
                if (ColorMatchFound != true) {
                    WinLoseManager.WinLoseManagerInstance.EndLevel(false);
                    AudioManager.AudioManagerInstance.PlaySFX(obstacle.GetComponent<Obstacle>().ObstacleSFX);
                    break;
                }
                break;

            case var _ when color == Color.green:
                if (ColorMatchFound != true) {
                    player.GetComponent<PlayerMovement>().TriggerMoveSpeedChange(1.25f);
                    CanvasManager.CanvasManagerInstance.GameplayHUD.GetComponent<ObstacleEffectsHUDScript>().DisplayEffectText("Slowdown", Color.green);
                    AudioManager.AudioManagerInstance.PlaySFX(obstacle.GetComponent<Obstacle>().ObstacleSFX);
                    break;
                }
                break;

            case var _ when color == Color.yellow:
                if (ColorMatchFound != true) {
                    player.GetComponent<PlayerMovement>().TriggerControlReverse();
                    CanvasManager.CanvasManagerInstance.GameplayHUD.GetComponent<ObstacleEffectsHUDScript>().DisplayEffectText("Reverse Movement", Color.yellow);
                    AudioManager.AudioManagerInstance.PlaySFX(obstacle.GetComponent<Obstacle>().ObstacleSFX);
                    break;
                }
                break;

            case var _ when color == Color.orange:
                if (ColorMatchFound != true) {
                    // Calculate direction of push
                    Vector2 PushDirection = -((Vector2)obstacle.transform.position - (Vector2)player.transform.position).normalized;
                    AudioManager.AudioManagerInstance.PlaySFX(obstacle.GetComponent<Obstacle>().ObstacleSFX);

                    // Apply KnockBack
                    player.GetComponent<PlayerMovement>().ApplyKnockback(PushDirection * 2.5f);
                    break;
                }
                break;

            case var _ when color == Color.purple: //checks for purple
                if (ColorMatchFound == true) { //unlike previous cases, this checks if a match IS found
                    Physics2D.IgnoreCollision(
                        player.GetComponent<BoxCollider2D>(),
                        obstacle.GetComponent<BoxCollider2D>(),
                        true
                    );

                    // Optional: re-enable collision after a delay
                    StartCoroutine(ReenableCollision(
                        player.GetComponent<BoxCollider2D>(),
                        obstacle.GetComponent<BoxCollider2D>(),
                        5.1f // delay in seconds
                    ));
                    break;
                }
                AudioManager.AudioManagerInstance.PlaySFX(obstacle.GetComponent<Obstacle>().ObstacleSFX);
                break;

            default:
                break;
                }
        }
    
    // Add this coroutine in the same script
    private IEnumerator ReenableCollision(Collider2D col1, Collider2D col2, float delay) {
        yield return new WaitForSeconds(delay);
        if(col1 && col2) {
            Physics2D.IgnoreCollision(col1, col2, false);
        }
    }
}
