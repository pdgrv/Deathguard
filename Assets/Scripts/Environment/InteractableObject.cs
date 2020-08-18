using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    private bool _isUsed = false;
    private Vector3 _defaultScale;

    private void Start()
    {
        gameObject.layer = 11;
        _defaultScale = transform.localScale;
    }

    public void Activate(Player player)
    {
        if (!_isUsed)
        {
            _isUsed = true;
            Use(player);
        }
    }

    public void Highlight()
    {
        transform.localScale *= 1.1f;
    }

    public void Lowlight()
    {
        transform.localScale = _defaultScale;
    }

    protected abstract void Use(Player player);
}
