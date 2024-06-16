using UnityEngine;

using BehaviourTree;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private Transform transform;
    private NavMeshAgent nav;
    private GuardBT guardBT;
    // public Transform target;

    public TaskGoToTarget(Transform _transform, NavMeshAgent _nav, GuardBT _guard)
    {
        transform = _transform;
        nav = _nav;
        guardBT = _guard;
    }

    public override NodeState Evaluate()
    {
        //tekst aanpassen boven het hoofd van de ai
        guardBT.StateText.text = "TaskGoToTarget";

        // Transform target = (Transform)GetData(GuardBT.Settings.TargetStr);
        Transform target = blackboard.GetVariable<Transform>(GuardBT.Settings.TargetStr);
        UnityEngine.Debug.Log("" + target);
        if (target == null)
        {
            Debug.LogError($"Target not found using key {GuardBT.Settings.TargetStr}. Failing attack range check.");
            state = NodeState.FAILURE;
            return state;
        }
        float dist = Vector3.Distance(transform.position, target.position);
        if (guardBT.EquippedItems.Count == 0)
        {
            state = NodeState.RUNNING;
            return state;
        }
        else
        {
            //hier nog een extra flag switch, omdat het soms voorkomt dat de ai niet langs de check node komt.
            var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
            GlobalBlackboard.Instance.SetVariable(str, true);
        }
        //checks if guard is near the target
        if (dist > GuardBT.Settings.StopDist)
        {
            nav.SetDestination(target.position);
        }


        //checks if target is still in range.
        if (Vector3.Distance(transform.position, target.position) >= GuardBT.Settings.PerceptionRadius)
        {
            ClearData(GuardBT.Settings.TargetStr);
            // Debug.Log("out of range");
            // var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
            // GlobalBlackboard.Instance.SetVariable(str, false);
            state = NodeState.FAILURE; //als de target out of range is dan mag de node niet verder.
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
