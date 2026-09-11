using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private float _speedBonus = 0;
    private bool _isPressedJumpKey => Input.GetKey(_jumpKey);
    private bool _canJump;
    private float _pitch;
    private Rigidbody _rigidbody;

    public float FinalMoveSpeed
    {
        get
        {
            return _moveSpeed * (1 + _speedBonus);
        }
    }

    public float SpeedBonus
    {
        get
        {
            return _speedBonus;
        }

        set
        {
            _speedBonus = value;
        }
    }

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        CheckCanJump();
    }

    public void Rotate()
    {
        Vector3 input = ReadRotateInput() * _mouseSensitivity;

        transform.Rotate(0, input.y, 0, Space.Self);

        _pitch = Mathf.Clamp(_pitch + input.x, _minPitch, _maxPitch);

        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    public void Move()
    {
        Vector3 input = ReadMoveInput();

        Vector3 direction = transform.right * input.x + transform.forward * input.z;

        Vector3 newVelocity = new Vector3(
            direction.x * FinalMoveSpeed,
            _rigidbody.velocity.y,
            direction.z * FinalMoveSpeed
            );

        _rigidbody.velocity = newVelocity;
    }

    public void ResetSpeedBonus()
    {
        _speedBonus = 0;
    }

    public void Jump()
    {
        if (!_isPressedJumpKey || !_canJump) return;

        _canJump = false;
        _rigidbody.velocity = new Vector3(0, _jumpPower, 0);
    }

    private void CheckCanJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.1f))
        {
            _canJump = true;
        }
    }

    private Vector3 ReadRotateInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        return new Vector3(-y, x, 0);
    }

    private Vector3 ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z).normalized;
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
