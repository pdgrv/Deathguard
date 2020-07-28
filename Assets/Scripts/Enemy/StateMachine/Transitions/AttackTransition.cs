using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTransition : Transition
{
    private bool _isAttackNow;    

    private void Update()
    {
        if (TargetDistance <= NavMesh.stoppingDistance)
        {
            NeedTransit = true;
        }
    }
}
