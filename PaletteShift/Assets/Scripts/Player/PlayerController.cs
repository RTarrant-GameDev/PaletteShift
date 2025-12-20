using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private InputAction MoveAction;
    private InputAction JumpAction;
    private InputAction PauseAction;
    private Vector2 MoveInput;
    public bool IsGrounded;

    [Header("ExternalComponents")]
    private FiniteStateMachine PlayerMovementFSM;
    private PlayerJumping PlayerJumpComponent;
    private PlayerMovement PlayerMovementComponent;
    private bool JumpPressed;

    private void Awake()
    {
        PlayerMovementFSM = GetComponent<FiniteStateMachine>();
        PlayerMovementComponent = GetComponent<PlayerMovement>();
        PlayerJumpComponent = GetComponent<PlayerJumping>();

        MoveAction = InputSystem.actions.FindAction("Move");
        JumpAction = InputSystem.actions.FindAction("Jump");
        PauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void OnEnable()
    {
        if (PauseAction != null)
        {
            PauseAction.performed += OnPausePerformed;
            PauseAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (PauseAction != null)
        {
            PauseAction.performed -= OnPausePerformed;
            PauseAction.Disable();
        }
    }


    private void Update()
    {
        // Check if grounded
        IsGrounded = PlayerJumpComponent.IsGrounded();

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

            if (MoveInput != Vector2.zero)
            {
                PlayerMovementFSM.ChangeState("Move");
            }
            else if (MoveInput == Vector2.zero)
            {
                PlayerMovementFSM.ChangeState("Idle");
            }

            PlayerMovementComponent.MovePlayer(MoveInput);
        }

        // Jump
        if (JumpPressed && IsGrounded)
        {
            PlayerMovementFSM.ChangeState("Jump");
            PlayerJumpComponent.Jump(); // Trigger the upward force here
        }
        else if (!IsGrounded)
        {
            // Check vertical velocity to determine if we're going up or down
            if (GetComponent<Rigidbody2D>().linearVelocity.y > 0.1f)
            {
                PlayerMovementFSM.ChangeState("Jump");
            }
            else
            {
                PlayerMovementFSM.ChangeState("Fall");
            }
        }

        JumpPressed = false;
    }
    
        // Handles Pause input
    private void OnPausePerformed(InputAction.CallbackContext context) {
        if (CanvasManager.CanvasManagerInstance != null)
        {
            GameSystemManagerScript.GameSystemManagerInstance.PauseGame();
        }
    }
}
