using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInputAction;
    [SerializeField] private Vector2 _moveInput;

    private void OnEnable()
    {
        _moveInputAction.action.Enable();
        _moveInputAction.action.performed += OnMove;
        _moveInputAction.action.canceled += OnMoveStop;
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
