using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Armor))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _expNeeded = 15;
    [SerializeField] private int _expNeededMultiply = 2;
    [SerializeField] private int _increaseHpOnLvlup = 10;

    private int _currentHealth;
    private int _level = 1;
    private int _currentExp = 0;
    private int _score;

    public bool HasKey { get; set; }

    private Armor _armor;

    private void Start()
    {
        _armor = GetComponent<Armor>();

        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        damage -= _armor.Value;
        damage = damage < 0 ? 0 : damage;

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyHeal(int heal)
    {
        heal = _currentHealth <= _maxHealth - heal ? heal : _maxHealth - _currentHealth;

        _currentHealth += heal;
    }

    private void Die()
    {
        Debug.Log("вы проиграли.");
    }

    public void AddReward(int exp, int score)
    {
        _score += score;

        _currentExp += exp;
        if (_currentExp >= _expNeeded)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _currentExp = _currentExp - _expNeeded;
        _expNeeded *= _expNeededMultiply;

        _level++;
        _maxHealth += _increaseHpOnLvlup;        
    }
}
