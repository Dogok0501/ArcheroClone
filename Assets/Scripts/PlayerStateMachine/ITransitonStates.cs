using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransitonStates
{
    void Initialize();

    void UpdateAction();

    void ToIdleState();

    void ToMoveState();

    void ToAttackState();
}
