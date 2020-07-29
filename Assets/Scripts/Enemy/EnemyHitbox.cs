using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Enemy _enemy;
    private int _damage;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();

        _damage = _enemy.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _damage = _enemy.Damage;

            player.ApplyDamage(_damage);
            Debug.Log(player.name + " получил пизды");
        }
    }
}
