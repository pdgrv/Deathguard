using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStand : Shop
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private int _damageIncrease = 2;

    protected override bool BuyItem(Player player)
    {
        player.GetComponent<Weapon>().AddModifier(_damage);
        return true;
    }

    protected override void UpdateItem()
    {
        base.UpdateItem();
        _damage += _damageIncrease;
    }
}
