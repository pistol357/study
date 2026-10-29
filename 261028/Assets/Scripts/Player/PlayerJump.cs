using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 점프 입력을 받아 지면에 있을 때만 Rigidbody로 플레이어를 점프시킨다.
/// </summary>
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _groundCheckDistance = 1.1f;

    private Rigidbody _rigidbody;

    private void Awake() => CacheComponents();

    private void OnEnable()
    {
        _jumpAction.action.performed += OnJump;
        _jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        _jumpAction.action.performed -= OnJump;
        _jumpAction.action.Disable();
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;

        // 낙하 중 누적된 y 속도와 무관하게 항상 같은 높이로 뛰도록 y 속도를 덮어쓴다
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _jumpSpeed, _rigidbody.linearVelocity.z);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundCheckDistance);
    }
}
