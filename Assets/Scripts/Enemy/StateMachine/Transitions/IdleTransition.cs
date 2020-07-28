using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTransition : Transition
{
    [SerializeField] private float _lookRadius;
    public float LookRadius => _lookRadius;
    
    private void Update()
    {
        if (TargetDistance > _lookRadius)
        {
            NeedTransit = true;
        }
    }
}
