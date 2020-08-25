using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    private Vector3 _defaultScale;
    protected bool _isUsed = false;

    protected virtual void Start()
    {
        gameObject.layer = 11;
        _defaultScale = transform.localScale;
    }

    public void Interact(Player player)
    {
        if (!_isUsed)
        {
            _isUsed = true;
            Activate(player);
        }
    }

    public void Highlight()
    {
        if (!_isUsed)
            transform.localScale *= 1.1f;
    }

    public void Lowlight()
    {
        transform.localScale = _defaultScale;
    }

    protected abstract void Activate(Player player);
}

