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
        string str = GlobalBlackboard.Instance.AttackingPlayerStr;
        Transform target = (Transform)GetData(GuardBT.settings.TargetStr);

        float dist = Vector3.Distance(transform.position, target.position);
        Debug.Log(dist);
        if (dist <= GuardBT.settings.StopDist + 0.2) //small offset because the enemy ai stops just shy of 1 from the player
        {
            //if this is true, the enemy is attacking.
            GlobalBlackboard.Instance.SetVariable(str, true);
        }

        //since this node is only called once the previous node has returned a succes
        //we can do our attack logic here (preferably in an enemymanager script).
        // Debug.Log("attacking");
        if (TestIsDead)
        {
            //if the target is dead, we clear the data and return to patrolling.
            state = NodeState.SUCCES;
            ClearData(GuardBT.settings.TargetStr);
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
