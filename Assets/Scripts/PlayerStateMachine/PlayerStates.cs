using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;
    public PlayerRaycast playerRaycast;

    IdleState _idleState;
    public IdleState idleState { get { return _idleState; ; } }

    MoveState _moveState;
    public MoveState moveState { get { return _moveState; ; } }

    AttackState _attackState;
    public AttackState attackState { get { return _attackState; ; } }

    public ITransitonStates currentState;

    public Transform firePosition;
    public GameObject bullet;       

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerRaycast = GetComponent<PlayerRaycast>();

        _idleState = new IdleState(this);
        _moveState = new MoveState(this);
        _attackState = new AttackState(this);

        currentState = idleState;
    }

    void Update()
    {
        currentState.UpdateAction();
    }

    public void CreateBullet()
    {
        Instantiate(bullet.gameObject, firePosition.position, firePosition.rotation);
        
        if (PlayerSkill.abllityList[0] > 1)
        {
            if (PlayerSkill.abllityList[0] > 3)
                    PlayerSkill.abllityList[0] = 2;

            for (int i = 1; i < PlayerSkill.abllityList[0]; i++)
                Invoke("CreateAdditionalBullet", 0.2f * i);
        }            
    }

    public void CreateAdditionalBullet()
    {
        Instantiate(bullet.gameObject, firePosition.position, firePosition.rotation);
    }
}
