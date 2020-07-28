using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private void Update()
    {
        NavMesh.SetDestination(Target.transform.position);
        Animator.SetBool("Walk", true);
    }
}
