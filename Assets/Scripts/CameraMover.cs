using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothSpeed = 0.2f;

    private Vector3 _offset;

    private void Start()
    {
        _offset = _player.position - transform.position;
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 desiredPosition = _player.position - _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
    }
}

