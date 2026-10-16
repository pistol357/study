using UnityEngine;

public sealed class DeadState : StateBase
{
    private PlayerController _controller;

    public DeadState(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Enter()
    {
        Debug.Log("<color=green>Dead 상태 진입</color>");
    }

    public override void Update()
    {
        Debug.Log("<color=green>Dead 상태</color>");
    }

    public override void Exit()
    {
    }
}
