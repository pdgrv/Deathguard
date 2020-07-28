using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;

    private SphereCollider _hitbox;

    private void Start()
    {
        _hitbox = GetComponentInChildren<SphereCollider>();
    }

    private float _lastAttackTime;

    private void Update()
    {
        Animator.SetBool("Walk", false);
        FaceTarget();

        if (_lastAttackTime <= 0)
        {
            StartCoroutine(Attack());
            _lastAttackTime = _attackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private IEnumerator Attack()
    {
        Animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.6f);
        _hitbox.enabled = true;
        yield return new WaitForFixedUpdate();
        _hitbox.enabled = false;
    }

    private void FaceTarget()
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, NavMesh.angularSpeed / 45 * Time.deltaTime);
    }
}
