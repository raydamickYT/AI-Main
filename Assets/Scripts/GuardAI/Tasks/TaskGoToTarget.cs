using UnityEngine;

using BehaviourTree;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private Transform transform;
    private NavMeshAgent nav;
    private GuardBT guard;
    // public Transform target;

    public TaskGoToTarget(Transform _transform, NavMeshAgent _nav, GuardBT _guard)
    {
        transform = _transform;
        nav = _nav;
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData(GuardBT.settings.TargetStr);
        UnityEngine.Debug.Log("" + target);
        if (target == null)
        {
            Debug.LogError($"Target not found using key {GuardBT.settings.TargetStr}. Failing attack range check.");
            state = NodeState.FAILURE;
            return state;
        }
        float dist = Vector3.Distance(transform.position, target.position);
        if (guard.EquippedItems.Count == 0)
        {
            state = NodeState.RUNNING;
            return state;
        }
        else
        {
            var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
            GlobalBlackboard.Instance.SetVariable(str, true);
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
            var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
            GlobalBlackboard.Instance.SetVariable(str, false);
            state = NodeState.FAILURE; //als de target out of range is dan mag de node niet verder.
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
    public override void OnEnter()
    {
        // GlobalBlackboard.Instance.SetVariable("ShouldHide", true); //als de enemy begint met zn achtervolging is dit true
        //we zetten deze variabele weer op false in: TaskPatrol.cs
        guard.StateText.text = "TaskGoToTarget";
        base.OnEnter();
    }
}
