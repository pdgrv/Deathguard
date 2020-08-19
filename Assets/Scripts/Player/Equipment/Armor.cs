using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonHeroes;
using UnityEngine.Events;

public class Armor : MonoBehaviour
{
    [SerializeField] private int _startingArmor = 3;

    private SetCharacter _setCharacter;

    public int Value { get; private set; }
    public UnityAction<int> ArmorChanged;

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

        ChangeArmorStats(_startingArmor);
    }

    public bool IncreaseTier(int armorGive)
    {
        _setCharacter.RemoveItem(_setCharacter.itemGroups[0], _armorTier);
        _setCharacter.RemoveItem(_setCharacter.itemGroups[1], _armorTier);

        if (_armorTier < 3)
            _armorTier++;
        else
            return false;

        _setCharacter.AddItem(_setCharacter.itemGroups[0], _armorTier);
        _setCharacter.AddItem(_setCharacter.itemGroups[1], _armorTier);

        ChangeArmorStats(armorGive);
        return true;
    }

    private void ChangeArmorStats(int armorGive)
    {
        Value += armorGive;

        ArmorChanged?.Invoke(Value);
    }
}