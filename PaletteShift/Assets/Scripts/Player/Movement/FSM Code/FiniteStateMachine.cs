using UnityEngine;

public enum PlayerState
{
    IdleState,
    MoveState,
    JumpState,
    FallState
}

public class FiniteStateMachine : MonoBehaviour {
    public PlayerState CurrentState;
    public Animator PlayerAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        CurrentState = PlayerState.IdleState;
        PlayerAnimator = GetComponent<Animator>();
    }


    void Update() {
        // To be used for animations
        switch (CurrentState) {
            case PlayerState.IdleState:
                PlayerAnimator.SetBool("IsRunning", false);
                PlayerAnimator.SetBool("IsJumping", false);
                PlayerAnimator.SetBool("IsFalling", false);
                break;

            case PlayerState.MoveState:
                PlayerAnimator.SetBool("IsRunning", true);
                PlayerAnimator.SetBool("IsJumping", false);
                PlayerAnimator.SetBool("IsFalling", false);
                break;

            case PlayerState.JumpState:
                PlayerAnimator.SetBool("IsRunning", false);
                PlayerAnimator.SetBool("IsJumping", true);
                PlayerAnimator.SetBool("IsFalling", false);
                break;

            case PlayerState.FallState:
                PlayerAnimator.SetBool("IsRunning", false);
                PlayerAnimator.SetBool("IsJumping", false);
                PlayerAnimator.SetBool("IsFalling", true);
                break;

            default:
                break;
        }
    }

    public void ChangeState(string NewState) {
        switch (NewState) {
            case "Idle":
                CurrentState = PlayerState.IdleState;
                break;

            case "Move":
                CurrentState = PlayerState.MoveState;
                break;

            case "Jump":
                CurrentState = PlayerState.JumpState;
                break;

            case "Fall":
                CurrentState = PlayerState.FallState;
                break;

            default:
                break;
        }
    }
}
