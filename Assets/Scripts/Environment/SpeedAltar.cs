using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAltar : InteractableObject
{
    [SerializeField] private float _speedMultiplier = 1.1f;

    protected override void Use(Player player)
    {
        player.GetComponent<CharacterMovement>().IncreaseSpeed(_speedMultiplier);
    }
}
