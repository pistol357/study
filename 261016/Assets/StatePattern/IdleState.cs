using UnityEngine;
using UnityEngine.InputSystem;

public sealed class IdleState : StateBase
{
    private PlayerController _controller;

    public IdleState(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Enter()
    {
        Debug.Log("<color=blue>Idle 상태 진입</color>");
        _controller.AttackInputRef.action.performed += TransitionAttack;
    }

    public override void Update()
    {
        Debug.Log("<color=blue>Idle 상태</color>");
    }

    public override void Exit()
    {
        Debug.Log("<color=blue>Idle 상태 탈출</color>");
        _controller.AttackInputRef.action.performed -= TransitionAttack;
    }

    private void TransitionAttack(InputAction.CallbackContext context)
    {
        _controller.SetState(_controller.Attack);
    }
}
