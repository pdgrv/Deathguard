using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmorStand : Shop
{
    [SerializeField] private List<GameObject> _armors;

    private int _currentArmor;

    protected override void SellItem(Player player)
    {
        if (_currentArmor < _armors.Count)
        {
            Armor playerArmor = player.GetComponent<Armor>();
            playerArmor.IncreaseTier(_stat);
            _currentArmor++;
        }
        if (_currentArmor >= _armors.Count)
            Destroy(gameObject);
    }

    protected override void UpdateItem()
    {
        base.UpdateItem();

        foreach (GameObject armor in _armors)
            armor.SetActive(false);
        if (_currentArmor <= _armors.Count - 1)
            _armors[_currentArmor].SetActive(true);
    }
}