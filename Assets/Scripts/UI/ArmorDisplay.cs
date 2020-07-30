using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDisplay : Display
{
    [SerializeField] private Armor _armor;

    private void OnEnable()
    {
        _armor.ArmorChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _armor.ArmorChanged -= OnValueChanged;
    }
}
