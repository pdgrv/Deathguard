using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grindstone : InteractableObject
{
    [SerializeField] private int _damage = 5;

    protected override void Logic(Player player)
    {
        player.GetComponent<Weapon>().AddModifier(_damage);
    }
}
