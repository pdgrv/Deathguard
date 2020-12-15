using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _rotationSpeed = 12f;
    [SerializeField] private float _jumpSpeed = 12f;
    [SerializeField] private float _rollRange = 10f;
    [SerializeField] private float _rollSpeed = 5f;
    [SerializeField] private float _gravityModifier = 3.5f;

    private PlayerActions _playerActions;

    private float _speedMultiplier = 1f;

    private CharacterController _characterController;
    private Animator _animator;
    private Transform _camera;

    private float _gravity = -9.8f;
    private float _minFall = -1.5f;
    private float _vertSpeed;

    private float _horizontalInput;
    private float _verticalInput;

    private bool _canMove = true;

    private float _rollRangeLeft = 0f;

    private Vector3 _move;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _playerActions = GetComponent<PlayerActions>();

        _camera = Camera.main.transform;

        _vertSpeed = _minFall;
        _animator.SetFloat("SpeedMultiplier", _speedMultiplier);
    }

    private void Update()
    {
        Roll();

        if (_canMove)
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
            _move = Vector3.ClampMagnitude(_move * _speed * _speedMultiplier, _speed * _speedMultiplier);

            LookDirection();
        }
        _animator.SetFloat("Speed", _move.magnitude);

        if (_playerActions.IsAttack)
            _move = Vector3.zero;

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
            if (_vertSpeed < _gravity / 2)
            {
                _animator.SetBool("Falling", true);
            }
        }
        _move.y = _vertSpeed;

        _characterController.Move(_move * Time.deltaTime);
    }

    private void Roll()
    {
        if (Input.GetButtonDown("Fire2") && _characterController.isGrounded)
        {
            _animator.SetTrigger("Roll");

            _rollRangeLeft = _rollRange;

            _canMove = false;
        }

        if (_rollRangeLeft > 0) 
        {
            var moveDelta = _rollSpeed * Time.deltaTime;
            _characterController.Move(transform.forward * moveDelta);

            _rollRangeLeft -= moveDelta;
        }
        else
        {
            _canMove = true;
        }
    }

    private void LookDirection()
    {
        Quaternion tmp = _camera.rotation;
        _camera.eulerAngles = new Vector3(0, _camera.eulerAngles.y, 0);
        _move = _camera.TransformDirection(_move);
        _camera.rotation = tmp;

        Quaternion lookRotation = Quaternion.LookRotation(_move);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }

    public void IncreaseSpeed(float multiplier)
    {
        _speedMultiplier *= multiplier;
        _animator.SetFloat("SpeedMultiplier", _speedMultiplier);
    }
}
