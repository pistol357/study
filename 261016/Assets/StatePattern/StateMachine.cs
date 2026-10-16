using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private StateBase _current;
    private IStateEvent _stateEvent;

    private void Awake()
    {
        _stateEvent = GetComponent<IStateEvent>();
    }

    private void OnEnable()
    {
        _stateEvent.OnStateChange += ChangeState;
    }

    public void Update()
    {
        _current?.Update();
    }

    private void OnDisable()
    {
        _stateEvent.OnStateChange -= ChangeState;
    }

    public void ChangeState(StateBase state)
    {
        if (state == null || _current == state) return;

        _current?.Exit();
        _current = state;
        _current.Enter();
    }
}
