using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _expGived;
    [SerializeField] private int _moneyGived; // сделать рандом

    private Animator _animator;
    private CharacterController _controller;

    public int Exp => _expGived;
    public int Money => _moneyGived;

    public Player Target { get; private set; }

    public event UnityAction<Enemy> Dying;

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

    private void Die()
    {
        _controller.enabled = false;

        _animator.SetTrigger("Die");
        Debug.Log(transform.name + "Die...");

        Dying?.Invoke(this);

        DestroyComponents<State>();
        DestroyComponents<Transition>();

        Destroy(gameObject, 3f);
    }

    public void Init(Player target)
    {
        Target = target;
    }

    private void DestroyComponents<T>() where T : MonoBehaviour
    {
        T[] components = GetComponents<T>();
        foreach (T component in components)
        {
            Destroy(component);
        }
    }
}
