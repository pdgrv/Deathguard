using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;

    private List<int> _modifiers = new List<int>();

    public int Damage => _damage;

    public UnityAction<int> DamageChanged;

    private void Start()
    {
        DamageChanged?.Invoke(_damage);
    }

    public void AddModifier(int damage)
    {
        _modifiers.Add(damage);
        _damage += damage;
        DamageChanged?.Invoke(_damage);
    }
}
