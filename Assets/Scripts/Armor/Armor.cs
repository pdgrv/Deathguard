using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonHeroes;

public class Armor : MonoBehaviour
{
    private SetCharacter _setCharacter;

    private int _armor;
    private int _armorTier;

    private void Start()
    {
        _setCharacter = GetComponent<SetCharacter>();

        for (int n = 0; n < _setCharacter.itemGroups[0].items.Length; n++)
        {
            if (_setCharacter.HasItem(_setCharacter.itemGroups[0], n))
            {
                _armorTier = n;
            }
        }

        ChangeArmorStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            GetNextArmor();
        }
    }

    private void GetNextArmor()
    {
        _setCharacter.RemoveItem(_setCharacter.itemGroups[0], _armorTier);
        _setCharacter.RemoveItem(_setCharacter.itemGroups[1], _armorTier);

        if (_armorTier < 3)
            _armorTier++;
        else
            _armorTier = 0;

        _setCharacter.AddItem(_setCharacter.itemGroups[0], _armorTier);
        _setCharacter.AddItem(_setCharacter.itemGroups[1], _armorTier);

        ChangeArmorStats();
    }

    private void ChangeArmorStats()
    {
        switch (_armorTier)
        {
            case 0:
                _armor = 0;
                break;
            case 1:
                _armor = 5;
                break;
            case 2:
                _armor = 10;
                break;
            case 3:
                _armor = 15;
                break;
        }
    }
}