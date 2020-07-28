using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTransition : Transition
{
    private NavMeshAgent _navMesh;

    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (TargetDistance <= _navMesh.stoppingDistance)
        {
            NeedTransit = true; 
        }
    }
}
