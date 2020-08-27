using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Armor), typeof(Weapon))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _needExp = 15;
    [SerializeField] private int _needExpIncrease = 15;
    [SerializeField] private int _increaseHpOnLvlup = 10;

    private Armor _armor; // изменить все-таки на значения и events от armor weapon и в actions
    private Weapon _weapon;

    private int _currentHealth;
    private int _level = 1;
    private int _currentExp = 0;
    public int _money;//паблик для тестов

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int, int> ExpChanged;
    public event UnityAction<int> LevelChanged;
    public event UnityAction<int> MoneyChanged;

    public int TotalDamage { get; private set; }
    public bool IsAlive = true;

    public AudioClipManager Audio { get; private set; }

    private void Awake()
    {
        _armor = GetComponent<Armor>();
        _weapon = GetComponent<Weapon>();

        Audio = GetComponent<AudioClipManager>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        ExpChanged?.Invoke(_currentExp, _needExp);
        LevelChanged?.Invoke(_level);
        MoneyChanged?.Invoke(_money);
    }

    private void OnEnable()
    {
        _weapon.DamageChanged += OnDamageChanged;
    }

    private void OnDisable()
    {
        _weapon.DamageChanged -= OnDamageChanged;
    }

    private void OnDamageChanged(int weaponDamage)
    {
        TotalDamage = weaponDamage;
    }

    public void ApplyDamage(int damage)
    {
        damage -= _armor.Value;
        damage = damage < 0 ? 0 : damage;

        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        Audio.PlaySound("ApplyDamage");

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
        Audio.PlaySound("Die");
        IsAlive = false;
    }

    public void AddReward(int exp, int money)
    {
        _money += money;
        MoneyChanged?.Invoke(_money);

        _currentExp += exp;
        ExpChanged?.Invoke(_currentExp, _needExp);
        if (_currentExp >= _needExp)
        {
            LevelUp();
        }
    }

    public bool BuyItem(int cost)
    {
        bool IsBuy;
        if (_money >= cost)
        {
            _money -= cost;
            IsBuy = true;
        }
        else
        {
            IsBuy = false;
        }

        MoneyChanged?.Invoke(_money);
        return IsBuy;
    }

    private void LevelUp()
    {
        _currentExp = _currentExp - _needExp;
        _needExp += _needExpIncrease;
        ExpChanged?.Invoke(_currentExp, _needExp);

        _level++;
        LevelChanged?.Invoke(_level);
        Audio.PlaySound("Lvlup");

        _maxHealth += _increaseHpOnLvlup;
        _currentHealth += _increaseHpOnLvlup;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
