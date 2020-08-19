using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Shop
{
    [SerializeField] private int _healValue;

    protected override bool BuyItem(Player player)
    {
        player.ApplyHeal(_healValue);
        return true;
    }
}
