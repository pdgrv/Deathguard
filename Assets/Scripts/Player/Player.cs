using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Armor))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxExp = 15;
    [SerializeField] private int _maxExpMultiplyer = 2;
    [SerializeField] private int _increaseHpOnLvlup = 10;

    private Armor _armor; // изменить все-таки на значения и events от armor weapon и в actions

    private int _currentHealth;
    private int _level = 1;
    private int _currentExp = 0;
    private int _score;

    private bool _hasKey;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int, int> ExpChanged;
    public event UnityAction<int> LevelChanged;

    public bool IsAlive = true;

    private void Start()
    {
        _armor = GetComponent<Armor>();

        _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        ExpChanged?.Invoke(_currentExp, _maxExp);
        LevelChanged?.Invoke(_level);
    }

    public void ApplyDamage(int damage)
    {
        damage -= _armor.Value;
        damage = damage < 0 ? 0 : damage;

        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyHeal(int heal)
    {
        heal = _currentHealth <= _maxHealth - heal ? heal : _maxHealth - _currentHealth;

        _currentHealth += heal;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        Debug.Log("вы проиграли.");
        IsAlive = false;
    }

    public void AddReward(int exp, int score)
    {
        _score += score;

        _currentExp += exp;
        ExpChanged?.Invoke(_currentExp, _maxExp);
        if (_currentExp >= _maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _currentExp = _currentExp - _maxExp;
        _maxExp *= _maxExpMultiplyer;
        ExpChanged?.Invoke(_currentExp, _maxExp);

        _level++;
        LevelChanged?.Invoke(_level);

        _maxHealth += _increaseHpOnLvlup;
        _currentHealth += _increaseHpOnLvlup;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void FindKey()
    {
        _hasKey = true;
    }
}
