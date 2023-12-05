using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskAttackTarget : Node
{
    private bool TestIsDead = false;
    private Transform transform;
    public TaskAttackTarget(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData(GuardBT.settings.TargetStr);

        //since this node is only called once the previous node has returned a succes
        //we can do our attack logic here (preferably in an enemymanager script).
        // Debug.Log("attacking");
        if (TestIsDead)
        {
            //if the target is dead, we clear the data and return to patrolling.
            ClearData(GuardBT.settings.TargetStr);
        }
        // transform.LookAt(target.position);

        state = NodeState.RUNNING;
        return state;
    }
}
