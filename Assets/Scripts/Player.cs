using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;
    private int _level;
    private int _armor;
    private int _score;

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
    }
}
