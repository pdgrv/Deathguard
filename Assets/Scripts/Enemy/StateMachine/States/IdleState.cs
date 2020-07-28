using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private void Update()
    {
        NavMesh.ResetPath();
        Animator.SetBool("Walk", false);
    }
}
