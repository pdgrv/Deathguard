using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay = 2.5f;

    private SphereCollider _hitbox;
    public Coroutine AttackJob { get; private set; }

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
            if (AttackJob != null)
                StopCoroutine(AttackJob);
            AttackJob = StartCoroutine(Attack());

            _lastAttackTime = _attackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private IEnumerator Attack()
    {
        var WaitForSplitSecond = new WaitForSeconds(0.9f);

        Animator.ResetTrigger("ApplyDamage");
        Animator.SetTrigger("Attack");

        yield return WaitForSplitSecond;
        
        Collider[] hitColliders = Physics.OverlapSphere(_hitbox.transform.position, _hitbox.radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Player player))
            {
                player.ApplyDamage(_damage);
                Debug.Log(player.name + " получил пизды" + _damage);
            }
        }

        yield return WaitForSplitSecond;
        AttackJob = null;
    }

    private void FaceTarget()
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, NavMesh.angularSpeed / 45 * Time.deltaTime);
    }
}
