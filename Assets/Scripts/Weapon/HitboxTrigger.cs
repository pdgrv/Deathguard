using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTrigger : MonoBehaviour
{
    //private Player _player; //переделываем тип на stats
    [SerializeField] private int _enemyDamage = 10;
    private int _damage;

    private void Start()
    {
        //_player = GetComponentInParent<Player>();

        //_damage = _player.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            //_damage = _player.Damage;

            enemy.ApplyDamage(_enemyDamage);
            Debug.Log(enemy.name + " получил пизды");
        }
        else if (other.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_enemyDamage);
            Debug.Log(player.name + " отхватил от " + transform.name);            
        }
    }
}
