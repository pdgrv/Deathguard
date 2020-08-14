using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTransition : Transition
{
    private void Update()
    {
        if (Target != null && TargetDistance <= NavMesh.stoppingDistance && Target.IsAlive)
        {
            NeedTransit = true;
        }
    }
}
