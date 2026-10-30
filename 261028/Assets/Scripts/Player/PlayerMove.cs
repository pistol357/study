using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// WASD 입력을 받아 Rigidbody2D로 플레이어를 8방향 이동시킨다.
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rigidbody;

    private void Awake() => CacheComponents();

    private void OnEnable() => _moveAction.action.Enable();

    private void OnDisable() => _moveAction.action.Disable();

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = _moveAction.action.ReadValue<Vector2>().normalized;

        _rigidbody.linearVelocity = direction * _moveSpeed;
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
