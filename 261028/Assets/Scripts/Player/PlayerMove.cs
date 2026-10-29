using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// WASD 입력을 받아 Rigidbody로 플레이어를 월드 축 기준으로 이동시킨다.
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody _rigidbody;

    private void Awake() => CacheComponents();

    private void OnEnable() => _moveAction.action.Enable();

    private void OnDisable() => _moveAction.action.Disable();

    private void FixedUpdate() => Move();

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        Vector2 input = _moveAction.action.ReadValue<Vector2>();

        // y 속도를 덮어쓰면 중력에 의한 낙하가 멈추므로 기존 값을 유지한다
        _rigidbody.linearVelocity = new Vector3(input.x * _moveSpeed, _rigidbody.linearVelocity.y, input.y * _moveSpeed);
    }
}
