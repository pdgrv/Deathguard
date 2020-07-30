using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject
{
    protected override void Use(Player player)
    {
        player.HasKey = true;
    }
}
