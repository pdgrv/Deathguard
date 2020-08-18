using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransition : Transition
{
    private bool _isAttackNow;

    private void Update()
    {
        _isAttackNow = GetComponent<AttackState>().AttackJob != null; //hz norm ili ne norm

        if (TargetDistance > NavMesh.stoppingDistance && !_isAttackNow && Target.IsAlive)
        {
            NeedTransit = true;
        }
    }
}