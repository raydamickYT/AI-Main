using System.Buffers.Text;
using BehaviourTree;
using UnityEngine;

public class CheckEnemyInAttackRange : Node
{
    private Transform transform;
    private GuardBT guard;

    public CheckEnemyInAttackRange(Transform _transform, GuardBT _guard)
    {
        transform = _transform;
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        object t = GetData(GuardBT.settings.TargetStr);
        if (t == null)
        {
            Debug.LogError($"Target not found using key {GuardBT.settings.TargetStr}. Failing attack range check.");
            // No target found, so we cannot be in attack range
            state = NodeState.FAILURE;
            return state;
        }
        else
        {
            Debug.Log($"Target found using key {GuardBT.settings.TargetStr}.");
        }

        Transform target = (Transform)t;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= GuardBT.settings.AtkRange)
        {
            // Target is within attack range
            // Debug.Log("Target within attack range");
            state = NodeState.SUCCES;
        }
        else
        {
            // Target is not in attack range yet
            // Debug.Log("Target not in attack range, still running");
            state = NodeState.FAILURE;
        }
        return state;
    }

}
