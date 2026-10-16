using UnityEngine;

public sealed class AttackState : StateBase
{
    private PlayerController _controller;

    public AttackState(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Enter()
    {
        Debug.Log("<color=green>Attack 상태 진입</color>");
    }

    public override void Update()
    {
        Debug.Log("<color=green>Attack 상태</color>");
        _controller.SetState(_controller.Idle);
    }

    public override void Exit()
    {
        Debug.Log("<color=green>Attack 상태 탈출</color>");
    }
}
