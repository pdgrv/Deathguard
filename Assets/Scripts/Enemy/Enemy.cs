using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private Animator _animator;

    private int _currentHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth > 0)
        {
            _animator.SetTrigger("ApplyDamage");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("Die");
        Debug.Log(transform.name + "Die...");
    }
}
