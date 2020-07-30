﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    private bool _isUsed = false;

    private void Start()
    {
        gameObject.layer = 11;
    }

    public void Activate(Player player)
    {
        if (!_isUsed)
        {
            _isUsed = true;
            Logic(player);
        }
    }

    protected abstract void Logic(Player player);
}
