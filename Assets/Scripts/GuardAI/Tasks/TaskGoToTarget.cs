using UnityEngine;

using BehaviourTree;

public class TaskGoToTarget : Node
{
    private Transform transform;

    public TaskGoToTarget(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData(GuardBT.settings.targetStr);
        float dist = Vector3.Distance(transform.position, target.position);
        //checks if target is near the target
        if (dist > GuardBT.settings.StopDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, GuardBT.settings.persueSpeed(dist) * Time.deltaTime);
            transform.LookAt(target.position);
        }

        //checks if target is still in range.
        if (Vector3.Distance(transform.position, target.position) >= GuardBT.settings.PerceptionRadius)
        {
            ClearData(GuardBT.settings.targetStr);
            Debug.Log("out of range");
        }
        state = NodeState.RUNNING;
        return state;
    }
}
