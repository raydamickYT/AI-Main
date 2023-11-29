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
        Transform target = (Transform)GetData(GuardBT.target);

        if (Vector3.Distance(transform.position, target.position) > 0.01)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, GuardBT.speed * Time.deltaTime);
            transform.LookAt(target.position);
        }

        if (Vector3.Distance(transform.position, target.position) >= GuardBT.DetectionRange)
        {
            ClearData(GuardBT.target);
            Debug.Log("out of range");
        }
        state = NodeState.RUNNING;
        return state;
    }
}
