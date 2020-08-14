using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator),typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _expGived;
    [SerializeField] private int _scoreGived;

    private Animator _animator;
    private CharacterController _controller;

    public Player Target { get; private set; }
    public int Damage => _damage;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
            Die();
        else
            _animator.SetTrigger("ApplyDamage");       
    }

    private void Die()//доделать, нужно остановить state machine 
    {
        _controller.enabled = false;

        _animator.SetTrigger("Die");
        Debug.Log(transform.name + "Die...");

        Target.AddReward(_expGived, _scoreGived);
    }

    public void Init(Player target)
    {
        Target = target;
    }
}
