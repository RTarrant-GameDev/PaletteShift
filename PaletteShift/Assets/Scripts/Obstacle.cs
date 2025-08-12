using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Set obstacle color in Inspector")]
    public Color ObstacleColor;

    private void Start() {
        GetComponent<SpriteRenderer>().color = ObstacleColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<ColorMatchMechanic>().CompareColorWithObstacle(ObstacleColor) == false)
            {
                EventObserver.RaiseObstacleHit(ObstacleColor, collision.gameObject, collision);
            }
        }
    }
}
