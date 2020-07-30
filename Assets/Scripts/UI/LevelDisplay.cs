using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplay : Display
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.LevelChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= OnValueChanged;
    }
}
