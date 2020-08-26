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
    private PlayerInteractionBox _interactionBox;

    private Coroutine attackJob;
    private int attackID = 1;
    private float _lastAttackTime;
    public bool IsAttack { get; private set; }

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();

        _interactionBox = GetComponentInChildren<PlayerInteractionBox>();
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
            Use();
        }
    }

    private IEnumerator Attack(string attackNumber)
    {
        var WaitForSplitSecond = new WaitForSeconds(0.3f);

        _animator.SetTrigger(attackNumber);
        IsAttack = true;
        _player.Audio.PlaySound("Swing");

        yield return WaitForSplitSecond;
        Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, _attackRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(_player.TotalDamage);
                Debug.Log(enemy.name + " получил пизды" + _player.TotalDamage);
            }
        }

        _animator.ResetTrigger(attackNumber);

        yield return new WaitForSeconds(0.5f);
        IsAttack = false;
        attackID = 1;
        attackJob = null;
    }

    private void Use()
    {
        if (_interactionBox.Use(_player))
            _player.Audio.PlaySound("Use");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}
