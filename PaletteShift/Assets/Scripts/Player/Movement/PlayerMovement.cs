using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private Rigidbody2D PlayerRB;

    #region  "Coroutines"

    private Coroutine SpeedRoutine;
    private Coroutine ControlReverseRoutine;

#endregion
    public bool ReverseControls;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        ReverseControls = false;
    }

    public void MovePlayer(Vector2 Input)
    {
        if (ReverseControls == false)
        {
            PlayerRB.linearVelocity = new Vector2(Input.x * MoveSpeed, PlayerRB.linearVelocity.y);
        }
        else
        {
            PlayerRB.linearVelocity = -new Vector2(Input.x * MoveSpeed, PlayerRB.linearVelocity.y);
        }
    }


#region "Reverse Controls" 
    public void TriggerControlReverse()
    {
        SpeedRoutine = StartCoroutine(ReverseControlsForDuration());
    }

    private IEnumerator ReverseControlsForDuration()
    {
        ReverseControls = true;
        yield return new WaitForSeconds(7.5f);
        ReverseControls = false;
    }


#endregion

#region "Move Speed Change"   

    public void TriggerMoveSpeedChange(float MoveSpeedToSet)
    {
        SpeedRoutine = StartCoroutine(ChangeMoveSpeedForDuration(MoveSpeedToSet));
    }

    private IEnumerator ChangeMoveSpeedForDuration(float NewMoveSpeed)
    {
        MoveSpeed = NewMoveSpeed;
        yield return new WaitForSeconds(7.5f);
        MoveSpeed = 5f;
    }

#endregion
}
