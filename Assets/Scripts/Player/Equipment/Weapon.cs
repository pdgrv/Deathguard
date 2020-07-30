using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;

    private List<int> _modifiers = new List<int>();

    public int Damage => _damage;

    public void AddModifier(int damage)
    {
        _modifiers.Add(damage);
        _damage += damage;
    }
}
