using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 KnockBack;
    public float KnockBackDelay = 5f;
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

    
    public void ApplyKnockback(Vector2 force)
    {
        KnockBack = force;
    }

    public void MovePlayer(Vector2 Input)
    {
        float MovementX = ReverseControls ? -Input.x : Input.x;

        
        if(MovementX > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (MovementX < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        PlayerRB.linearVelocity = new Vector2(
            MovementX * MoveSpeed + KnockBack.x,
            PlayerRB.linearVelocity.y + KnockBack.y
        );

        KnockBack = Vector2.Lerp(KnockBack, Vector2.zero, KnockBackDelay * Time.deltaTime);
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
