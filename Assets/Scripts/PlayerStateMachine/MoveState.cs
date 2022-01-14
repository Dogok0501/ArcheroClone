using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : ITransitonStates
{
    PlayerStates player;

    public MoveState(PlayerStates player)
    {
        this.player = player;
    }

    public void Initialize()
    {

    }

    public void UpdateAction()
    {
        if (!player.playerMovement.isMoving)
            ToIdleState();
    }

    public void ToIdleState()
    {
        player.animator.SetBool("isMove", false);
        player.currentState = player.idleState;
    }

    public void ToMoveState()
    {

    }

    public void ToAttackState()
    {  

    }
}
