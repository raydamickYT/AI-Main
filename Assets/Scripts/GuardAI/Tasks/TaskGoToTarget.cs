using UnityEngine;

using BehaviourTree;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private Transform transform;
    private NavMeshAgent nav;
    private GuardBT guard;

    public TaskGoToTarget(Transform _transform, NavMeshAgent _nav, GuardBT _guard)
    {
        transform = _transform;
        nav = _nav;
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData(GuardBT.settings.TargetStr);
        if (target == null)
        {
            Debug.LogError($"Target not found using key {GuardBT.settings.TargetStr}. Failing attack range check.");
        }
        float dist = Vector3.Distance(transform.position, target.position);
        if (guard.EquippedItems.Count == 0)
        {
            state = NodeState.RUNNING;
            return state;
        }
        //checks if target is near the target
        if (dist > GuardBT.settings.StopDist)
        {
            nav.SetDestination(target.position);
        }

        //checks if target is still in range.
        if (Vector3.Distance(transform.position, target.position) >= GuardBT.settings.PerceptionRadius)
        {
            ClearData(GuardBT.settings.TargetStr);
            Debug.Log("out of range");
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
