using UnityEngine;

public class Obstacle : MonoBehaviour {
    [Header("Set obstacle color in Inspector")]
    public Color ObstacleColor;

    private void Start() {
        GetComponent<SpriteRenderer>().color = ObstacleColor;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EventObserver.RaiseObstacleHit(ObstacleColor, collision.gameObject, this.gameObject);
        }
    }
}
