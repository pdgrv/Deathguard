using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator),typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _lookRadius = 10f;

    private Transform _target;

    private NavMeshAgent _navMesh;
    private Animator _animator;

    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _target = GetComponent<Enemy>().Player.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance <= _lookRadius)
        {
            _navMesh.SetDestination(_target.position);
            _animator.SetBool("Walk", true);

            if (distance <= _navMesh.stoppingDistance)
            {
                _animator.SetBool("Walk", false);
                FaceTarget();
                //attack
            }
        }
        else
        {
            _animator.SetBool("Walk", false);
            _navMesh.ResetPath();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _navMesh.angularSpeed / 45 * Time.deltaTime);
    }

}
