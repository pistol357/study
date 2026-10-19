using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "Chase [Target] at [Speed]", category: "Action", id: "1b2ce6445baffec0d45b9e8ddc66f682")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Speed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Transform self = GameObject.transform;
        Transform target = this.Target.Value.transform;
        Vector3 toTarget = target.position - self.position;

        self.LookAt(target);
        self.position += toTarget.normalized * Speed.Value * Time.deltaTime;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

