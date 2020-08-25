using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransition : Transition
{
    private AttackState _attackState;
    private bool _isAttackNow;

    private void Start()
    {
        _attackState = GetComponent<AttackState>();
    }

    private void Update()
    {
        if (TargetDistance > NavMesh.stoppingDistance && !_attackState.IsAttack && Target.IsAlive)
        {
            NeedTransit = true;
        }
    }
}