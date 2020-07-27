using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;
    private int _level;
    private int _armor;
    [SerializeField] private int _totalDamage;

    private Animator _animator;
    private Weapon _weapon;
    private Collider _weaponCollider;
    private SphereCollider _hitbox;

    [SerializeField] private float _attackDelay = 0.3f;
    private Coroutine attackJob;
    private int attackID = 1;
    private float _lastAttackTime;

    private int _score;

    private void Start()
    {
        _animator = GetComponent<Animator>();        

        _hitbox = GetComponentInChildren<SphereCollider>();
        _hitbox.enabled = false;

        _currentHealth = _maxHealth;
    }

    private void Update()
    {        
        if (Input.GetButtonDown("Fire1") && _lastAttackTime <=0)
        {
            if (attackID == 1)
            {
                if (attackJob != null) StopCoroutine(attackJob);
                attackJob = StartCoroutine(Attack("Attack1"));
                attackID = 2;

            }
            else if (attackID == 2)
            {
                if (attackJob != null) StopCoroutine(attackJob);                
                attackJob = StartCoroutine(Attack("Attack2"));
                attackID=1;
            }
            _lastAttackTime = _attackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
    }

    private IEnumerator Attack(string attackNumber) //надеюсь, из-за такого никто не умрет.
    {
        _animator.SetTrigger(attackNumber);
        
        yield return new WaitForSeconds(0.4f);
        _hitbox.enabled = true;
        yield return new WaitForFixedUpdate();
        _hitbox.enabled = false;

        yield return new WaitForSeconds(0.6f);
        attackID = 1;
    }
}
