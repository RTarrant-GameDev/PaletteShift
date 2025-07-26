using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    private InputAction MoveAction;
    private InputAction JumpAction;
    private Vector2 MoveInput;
    public bool IsGrounded;

    [Header("ExternalComponents")]
    private FiniteStateMachine PlayerMovementFSM;
    private PlayerJumping PlayerJumpComponent;
    private PlayerMovement PlayerMovementComponent;
    private bool JumpPressed;

    private void Awake() {
        PlayerMovementFSM = GetComponent<FiniteStateMachine>();
        PlayerMovementComponent = GetComponent<PlayerMovement>();
        PlayerJumpComponent = GetComponent<PlayerJumping>();

        MoveAction = InputSystem.actions.FindAction("Move");
        JumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update() {
        // Check if grounded
        IsGrounded = PlayerJumpComponent.IsGrounded();

        if (JumpAction != null && JumpAction.WasPressedThisFrame() && IsGrounded) {
            JumpPressed = true;
        }
    }

    private void FixedUpdate() {
        if (MoveAction != null) {
            // Move player
            MoveInput = MoveAction.ReadValue<Vector2>();

            if (MoveInput != Vector2.zero) {
                PlayerMovementFSM.ChangeState("Move");
            }
            else if (MoveInput == Vector2.zero) {
                PlayerMovementFSM.ChangeState("Idle");
            }

            PlayerMovementComponent.MovePlayer(MoveInput);
        }

        // Jump
        if (JumpPressed && IsGrounded) {
            PlayerJumpComponent.Jump();
        }
        else if (!IsGrounded) {
            PlayerMovementFSM.ChangeState("Jump");
        }

        JumpPressed = false;
    }
}
