using System;
using UnityEngine;

public interface IStateEvent
{
    public Action<StateBase> OnStateChange { get; set; }
}
