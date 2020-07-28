using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IdleTransition))]
public class MoveTransition : Transition
{
    private float _followRadius = 10f;

    private bool _isAttackNow;

    private void Start()
    {
        _followRadius = GetComponent<IdleTransition>().LookRadius;
    }

    private void Update()
    {
        _isAttackNow = GetComponent<AttackState>().AttackJob != null;

        if (TargetDistance <= _followRadius && TargetDistance > NavMesh.stoppingDistance && !_isAttackNow)
        {
            NeedTransit = true;
        }
    }
}