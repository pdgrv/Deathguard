using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;
    public State TargetState => _targetState;

    protected Player Target { get; private set; }
    protected float TargetDistance { get; private set; }

    public bool NeedTransit { get; protected set; }

    private void FixedUpdate()
    {
        TargetDistance = Vector3.Distance(Target.transform.position, transform.position);
    }

    public void Init(Player target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
