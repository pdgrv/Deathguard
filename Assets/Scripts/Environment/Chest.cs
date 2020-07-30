using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : InteractableObject
{
    protected override void Logic(Player player)
    {
        player.GetComponent<Armor>().IncreaseTier();
    }
}