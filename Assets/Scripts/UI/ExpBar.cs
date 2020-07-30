using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.ExpChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.ExpChanged -= OnValueChanged;
    }
}
