using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStand : Shop
{
    protected override void SellItem(Player player)
    {
        player.GetComponent<Weapon>().AddModifier(_stat);
    }
}
