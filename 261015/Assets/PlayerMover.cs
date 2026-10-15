using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    private Vector2 _moveInput;
    private MyInputActions _inputActions;
    [SerializeField] private InputActionReference _moveInputAction;

    private void Awake()
    {
        _inputActions = new MyInputActions();
    }

    private void OnEnable()
    {
        _moveInputAction.action.Enable();
        _moveInputAction.action.performed += OnMove;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log(_moveInput);
    }

    public void OnMoveStop(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log(_moveInput);
    }

    public void OnJump()
    {
        Debug.Log("OnJump");
    }
}
