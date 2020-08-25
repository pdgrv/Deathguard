using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStand : Shop
{
    [SerializeField] private int _healValue;

    protected override void SellItem(Player player)
    {
        player.ApplyHeal(_healValue);
    }
}
