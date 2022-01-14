using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ITransitonStates
{
    PlayerStates player;

    public IdleState(PlayerStates player)
    {
        this.player = player;
    }

    public void Initialize()
    {
        
    }

    public void UpdateAction()
    {
        if (player.playerMovement.isMoving)
            ToMoveState();

        if (player.playerRaycast.enemySpotted)
            ToAttackState();
    }

    public void ToIdleState()
    {

    }

    public void ToMoveState()
    {
        player.animator.SetBool("isMove", true);
        player.currentState = player.moveState;
    }

    public void ToAttackState()
    {
        player.animator.SetBool("isMove", false);
        player.animator.SetBool("isAttack", true);
        player.currentState = player.attackState;
    }
}
