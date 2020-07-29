using CartoonHeroes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    private SetCharacter _setCharacter;

    [SerializeField] private int _armorTier;

    private void Start()
    {
        _setCharacter = GetComponent<SetCharacter>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("I"))
        {
            if (_armorTier >= 3)
                _armorTier = 0;

            _setCharacter.RemoveItem(_setCharacter.itemGroups[0], _armorTier);
            _setCharacter.RemoveItem(_setCharacter.itemGroups[1], _armorTier);

            _armorTier++;

            _setCharacter.AddItem(_setCharacter.itemGroups[0], _armorTier);
            _setCharacter.AddItem(_setCharacter.itemGroups[1], _armorTier);
        }
    }
}
