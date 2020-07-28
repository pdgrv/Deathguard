using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 0.45f;

    private Animator _animator;
    private SphereCollider _hitbox;

    private Coroutine attackJob;
    private int attackID = 1;
    private float _lastAttackTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _hitbox = GetComponentInChildren<SphereCollider>();
        _hitbox.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _lastAttackTime <= 0)
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
                attackID = 1;
            }

            _lastAttackTime = _attackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private IEnumerator Attack(string attackNumber)
    {
        _animator.SetTrigger(attackNumber);        

        yield return new WaitForSeconds(0.3f);
        _hitbox.enabled = true;
        yield return new WaitForFixedUpdate();
        _hitbox.enabled = false;

        yield return new WaitForSeconds(0.75f);
        attackID = 1;
        _animator.ResetTrigger(attackNumber);
    }
}
