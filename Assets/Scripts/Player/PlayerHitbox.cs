using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private Player _player;
    private int _damage;

    private void Start()
    {
        _player = GetComponentInParent<Player>();

        _damage = _player.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _damage = _player.Damage;

            enemy.ApplyDamage(_damage);
            Debug.Log(enemy.name + " получил пизды");
        }
    }
}
