using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IdleTransition))]
public class MoveTransition : Transition
{
    private float _followRadius = 10f;

    private void Start()
    {
        _followRadius = GetComponent<IdleTransition>().LookRadius;
    }
    private void Update()
    {
        if (TargetDistance <= _followRadius && TargetDistance > NavMesh.stoppingDistance)
        {
            NeedTransit = true;
        }
    }
}
