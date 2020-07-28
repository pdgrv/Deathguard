using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveState : State
{
    private NavMeshAgent _navMesh;        

    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();        
    }

    private void Update()
    {
        _navMesh.SetDestination(Target.transform.position);
        _animator.SetBool("Walk", true);
    }
}
