using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings.Frustum;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Search For Player", story: "[Agent] Searches For [Target]", category: "Action", id: "80ea19c837b8771d610f590106186145")]
public partial class SearchingAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    FOVCone _fovCone;
    protected override Status OnStart()
    {
        if(Agent.Value == null) return Status.Failure;
        
        _fovCone = Agent.Value.GetComponent<FOVCone>();
        if(_fovCone == null)
        {
            Debug.Log("Fov script is null");
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(_fovCone.targetInSight == true)
        {
            Target.Value = _fovCone.detectedTarget;
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        _fovCone = null;
    }
}

