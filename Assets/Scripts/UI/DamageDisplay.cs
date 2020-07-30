using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : Display
{
    [SerializeField] private Weapon _weapon;

    private void OnEnable()
    {
        _weapon.DamageChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _weapon.DamageChanged -= OnValueChanged;
    }
}
