using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ITransitonStates
{
    PlayerStates player;
    float attackCooldown = Define.ATTACK_COOLDOWN;
    float timer = 0.0f;

    public AttackState(PlayerStates player)
    {
        this.player = player;
    }

    public void Initialize()
    {

    }

    public void UpdateAction()
    {
        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            player.CreateBullet();
            timer = 0.0f;
        }            

        if (player.playerMovement.isMoving)
            ToMoveState();

        if (!player.playerMovement.isMoving && !player.playerRaycast.enemySpotted)
            ToIdleState();
    }

    public void ToIdleState()
    {
        player.animator.SetBool("isMove", false);
        player.animator.SetBool("isAttack", false);
        player.currentState = player.idleState;
    }

    public void ToMoveState()
    {
        player.animator.SetBool("isMove", true);
        player.animator.SetBool("isAttack", false);
        player.currentState = player.moveState;      
    }

    public void ToAttackState()
    {

    }    
}
