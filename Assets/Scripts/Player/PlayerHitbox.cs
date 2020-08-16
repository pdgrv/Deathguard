using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private float _hitboxRadius = 0.75f;

    private SphereCollider _collider;

    //private Weapon _weapon;

    private void Start()
    {
       // _weapon = GetComponentInParent<Weapon>();
        _collider = GetComponent<SphereCollider>();

        _collider.radius = _hitboxRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out Enemy enemy))
        //{
        //    enemy.ApplyDamage(_weapon.Damage);
        //    Debug.Log(enemy.name + " получил пизды");
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _hitboxRadius);
    }
}
