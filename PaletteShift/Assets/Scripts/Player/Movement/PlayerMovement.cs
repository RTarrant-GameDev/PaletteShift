using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float MoveSpeed = 5f;
    private Rigidbody2D PlayerRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector2 Input) {
        PlayerRB.linearVelocity = new Vector2(Input.x * MoveSpeed, PlayerRB.linearVelocity.y);
    }
}
