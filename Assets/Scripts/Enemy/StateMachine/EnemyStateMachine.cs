using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(Animator), typeof(NavMeshAgent))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _fistState;

    private Enemy _enemy;
    private Player _target;
    private Animator _animator;
    private NavMeshAgent _navMesh;

    private State _currentState;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = _enemy.Player;
        _animator = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();

        Reset(_fistState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_target, _animator, _navMesh);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;
        _currentState.Enter(_target, _animator, _navMesh);
    }    
}
