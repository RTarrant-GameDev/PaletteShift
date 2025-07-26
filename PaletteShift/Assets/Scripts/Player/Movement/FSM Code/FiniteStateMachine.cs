using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState {
    IdleState,
    MoveState,
    JumpState
}

public class FiniteStateMachine : MonoBehaviour {
    public PlayerState CurrentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        CurrentState = PlayerState.IdleState;
    }


    void Update() {
        // To be used for animations
        switch (CurrentState) {
            case PlayerState.IdleState:
                break;

            case PlayerState.MoveState:
                break;

            case PlayerState.JumpState:
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

            default:
                break;
        }
    }
}
