using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(Player))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 0.45f;

    private Player _player;
    private Animator _animator;
    private SphereCollider _hitbox;

    private Coroutine attackJob;
    private int attackID = 1;
    private float _lastAttackTime;

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();

        _hitbox = GetComponentInChildren<SphereCollider>();
        _hitbox.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _lastAttackTime <= 0)
        {
            if (attackID == 1) //сократить этот момент
            {
                if (attackJob != null) StopCoroutine(attackJob);
                attackJob = StartCoroutine(Attack($"Attack{attackID}"));
                attackID = 2;
            }
            else if (attackID == 2)
            {
                if (attackJob != null) StopCoroutine(attackJob);
                attackJob = StartCoroutine(Attack($"Attack{attackID}"));
                attackID = 1;
            }

            _lastAttackTime = _attackDelay;
        }

        _lastAttackTime -= Time.deltaTime;

        if (Input.GetButtonDown("Fire3"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(_hitbox.transform.position, _hitbox.radius, 1 << 11); //так не получится сделать подсветку 
            foreach (Collider hitCollider in hitColliders)                                                        //при возможности активации.
            {
                if (hitCollider.TryGetComponent(out InteractableObject usedObject))
                {
                    usedObject.Activate(_player);
                }
            }
        }
    }

    private IEnumerator Attack(string attackNumber) //переделать на physics.overlap
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
