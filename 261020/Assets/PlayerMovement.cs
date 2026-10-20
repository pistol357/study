using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _gorundLayer;
    [SerializeField] private InputActionReference _mousePosition;
    [SerializeField] private InputActionReference _mouseLeftInput;
    [SerializeField] private Transform _goalTransform;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private NavMeshAgent _agent;

    private void OnEnable() => BindInputEvents();
    private void Start() => Init();
    private void Update() => Move();

    private void BindInputEvents()
    {
        _mouseLeftInput.action.Enable();
        _mousePosition.action.Enable();
        _mouseLeftInput.action.performed += OnMouseLeftButtonDown;
    }

    private void OnMouseLeftButtonDown(InputAction.CallbackContext context)
    {
        MoveTo();
    }

    private void MoveTo()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_mousePosition.action.ReadValue<Vector2>());
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, _gorundLayer))
        {
            _goalTransform.position = hit.point;
        }
    }

    private void Move()
    {
        // transform.position = Vector3.MoveTowards(transform.position, _goalTransform.position, Time.deltaTime);
        _agent.SetDestination(_goalTransform.position);
    }

    private void Init()
    {
        _goalTransform.position = transform.position;
    }
}
