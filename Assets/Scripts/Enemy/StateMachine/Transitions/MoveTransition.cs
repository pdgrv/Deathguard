using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTransition : Transition
{
    [SerializeField] private float _startFollowRadius = 10f;

    private NavMeshAgent _navMesh;

    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (TargetDistance <= _startFollowRadius && TargetDistance > _navMesh.stoppingDistance)
        {
            NeedTransit = true;
        }
    }
}
