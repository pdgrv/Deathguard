using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private float _jumpSpeed = 10f;
    [SerializeField] private float _gravityModifier = 3f;

    private float _gravity = -9.8f;
    private float _minFall = -1.5f;
    private float _vertSpeed;

    private CharacterController _characterController;
    private Animator _animator;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _move;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _vertSpeed = _minFall;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _move = Vector3.zero;

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            _move.x = _horizontalInput;
            _move.z = _verticalInput;
            _move = Vector3.ClampMagnitude(_move * _speed, _speed);

            Quaternion tmp = _camera.rotation;
            _camera.eulerAngles = new Vector3(0, _camera.eulerAngles.y, 0);
            _move = _camera.TransformDirection(_move);
            _camera.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(_move);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotationSpeed * Time.deltaTime);
        }
        _animator.SetFloat("Speed", _move.magnitude);

        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = _jumpSpeed;
                _animator.SetTrigger("Jump");
            }
            else
            {
                _vertSpeed = _minFall;
                _animator.ResetTrigger("Jump");
                _animator.SetBool("Falling", false);
            }
        }
        else
        {
            if (_vertSpeed > _gravity)
            {
                _vertSpeed += _gravity * _gravityModifier * Time.deltaTime;
            }
            _animator.SetBool("Falling", true);
        }
        _move.y = _vertSpeed;

        _characterController.Move(_move * Time.deltaTime);
    }
}
