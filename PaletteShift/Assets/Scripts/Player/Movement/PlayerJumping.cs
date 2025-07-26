using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    public float JumpForce = 5f;
    [Header("Ground Check")]
    public Transform GroundCheck;
    public float GroundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private Rigidbody2D PlayerRB;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
    }

    // Update is called once per frame
    public void Jump()
    {
        PlayerRB.linearVelocity = new Vector2(PlayerRB.linearVelocity.x, JumpForce);
    }
    
    private void OnDrawGizmosSelected() {
        if (GroundCheck != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        }
    }
}
