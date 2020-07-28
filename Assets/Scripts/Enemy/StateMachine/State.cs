using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Animator Animator { get; private set; }
    protected NavMeshAgent NavMesh { get; private set; }
    protected Player Target { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void Enter(Player target, Animator animator, NavMeshAgent navMesh)
    {
        if (enabled == false)
        {
            Target = target;
            Animator = animator;
            NavMesh = navMesh;

            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target, NavMesh);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;
            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}
