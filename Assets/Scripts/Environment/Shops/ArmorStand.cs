using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmorStand : Shop
{
    [SerializeField] private int _armor;
    [SerializeField] private int _armorIncrease;

    protected override bool BuyItem(Player player)
    {
        Armor playerArmor = player.GetComponent<Armor>();
        if (playerArmor.IncreaseTier(_armor))
            return true;
        else
            return false;
    }

    protected override void UpdateItem()
    {
        base.UpdateItem();
        _armor += _armorIncrease;
    }
}