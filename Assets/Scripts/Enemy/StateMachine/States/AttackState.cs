using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay = 2.5f;
    [SerializeField] private float _attackRadius = 0.5f;
    [SerializeField] private Transform _hitbox;

    private Coroutine AttackJob;

    public bool IsAttack { get; private set; }

    private float _lastAttackTime;

    private void Update()
    {
        Animator.SetBool("Walk", false);
        FaceTarget();

        if (_lastAttackTime <= 0)
        {
            if (AttackJob != null)
                StopCoroutine(AttackJob);
            AttackJob = StartCoroutine(Attack());

            _lastAttackTime = _attackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private void FaceTarget()
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, NavMesh.angularSpeed / 45 * Time.deltaTime);
    }

    private IEnumerator Attack()
    {
        var WaitForSplitSecond = new WaitForSeconds(0.95f);

        Animator.ResetTrigger("ApplyDamage");
        Animator.SetTrigger("Attack");

        IsAttack = true;

        yield return WaitForSplitSecond;

        Collider[] hitColliders = Physics.OverlapSphere(_hitbox.position, _attackRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Player player))
            {
                player.ApplyDamage(_damage);
                Debug.Log(player.name + " получил пизды" + _damage);
            }
        }

        yield return WaitForSplitSecond;
        IsAttack = false;
        AttackJob = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_hitbox.position, _attackRadius);
    }
}
