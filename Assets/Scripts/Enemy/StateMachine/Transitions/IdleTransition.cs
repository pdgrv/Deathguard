using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTransition : Transition
{
    private void Update()
    {
        if (!Target.IsAlive)
        {
            NeedTransit = true;
        }
    }
}
