using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;
    public State TargetState => _targetState;

    protected Player Target { get; private set; }
    protected NavMeshAgent NavMesh { get; private set; }

    protected float TargetDistance { get; private set; }

    public bool NeedTransit { get; protected set; }

    private void FixedUpdate()
    {
        TargetDistance = Vector3.Distance(Target.transform.position, transform.position);
    }

    public void Init(Player target, NavMeshAgent navMesh)
    {
        Target = target;
        NavMesh = navMesh;
    }

    private void OnEnable()
    {
        NeedTransit = false;        
    }
}
