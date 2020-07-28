using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTransition : Transition
{
    [SerializeField] private float _stopFollowRadius = 10f;

    private void Update()
    {
        if (TargetDistance > _stopFollowRadius)
        {
            NeedTransit = true;
        }
    }
}
