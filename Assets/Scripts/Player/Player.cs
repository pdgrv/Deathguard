using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;
    private int _level;
    private int _exp;
    [SerializeField] private int _totalDamage;
    private int _armor;
    private int _score;

    public int Damage => _totalDamage;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }       

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }    

    private void Die()
    {
        Debug.Log("вы проиграли.");
    }

    public void AddReward(int exp, int score)
    {
        _score += score;

        _exp += exp;
        if (_exp >= 100)
        {
            _exp = 0;
            _level++;
        }        
    }
}
