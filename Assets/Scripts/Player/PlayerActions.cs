using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Player))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 0.45f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius;

    private Player _player;
    private Animator _animator;
    private SphereCollider _actionbox;

    private Coroutine attackJob;
    private int attackID = 1;
    private float _lastAttackTime;

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();

        _actionbox = GetComponentInChildren<SphereCollider>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _lastAttackTime <= 0)
        {
            if (attackJob != null) StopCoroutine(attackJob);
            attackJob = StartCoroutine(Attack($"Attack{attackID}"));

            attackID = attackID == 1 ? 2 : 1;

            _lastAttackTime = _attackDelay;
        }

        _lastAttackTime -= Time.deltaTime;

        if (Input.GetButtonDown("Fire3"))
        {
            Interact();
        }
    }

    private IEnumerator Attack(string attackNumber)
    {
        _animator.SetTrigger(attackNumber);

        yield return new WaitForSeconds(0.3f);
        Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, _actionbox.radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(_player.TotalDamage);
                Debug.Log(enemy.name + " получил пизды" + _player.TotalDamage);
            }
        }

        yield return new WaitForSeconds(0.75f);
        attackID = 1;
        _animator.ResetTrigger(attackNumber);
    }

    private void Interact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_actionbox.transform.position, _actionbox.radius, 1 << 11);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out InteractableObject usedObject))
            {
                usedObject.Activate(_player);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}
