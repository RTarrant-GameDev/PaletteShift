using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5f;
    public float JumpForce = 5f;

    [Header("Ground Check")]
    public Transform GroundCheck;
    public float GroundCheckRadius = 0.2f;
    public LayerMask GroundLayer;

    private Rigidbody2D PlayerRB;
    private InputAction MoveAction;
    private InputAction JumpAction;
    private Vector2 MoveInput;
    private bool IsGrounded;
    private bool JumpPressed;

    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();

        MoveAction = InputSystem.actions.FindAction("Move");
        JumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        // Check if grounded
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);

        if (JumpAction != null && JumpAction.WasPressedThisFrame() && IsGrounded)
        {
            JumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (MoveAction != null)
        {
            // Move player
            MoveInput = MoveAction.ReadValue<Vector2>();
            PlayerRB.linearVelocity = new Vector2(MoveInput.x * MoveSpeed, PlayerRB.linearVelocity.y);
            
        }

        // Jump
        if (JumpPressed && IsGrounded)
        {
            PlayerRB.linearVelocity = new Vector2(PlayerRB.linearVelocity.x, JumpForce);
        }

        JumpPressed = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        }
    }
}
