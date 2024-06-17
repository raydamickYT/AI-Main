using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BehaviourTree;

public class TaskAttackTarget : Node
{
    private bool playerIsDead = false;
    private Transform transform;
    public TaskAttackTarget(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        string str = blackboard.AttackingPlayerStr;
        // Transform target = (Transform)GetData(GuardBT.Settings.TargetStr);
        Transform target = blackboard.GetVariable<Transform>(GuardBT.Settings.TargetStr);

        if (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.position);

            if (dist <= GuardBT.Settings.StopDist + 0.5) //small offset because the enemy ai stops just shy of 1 from the player
            {
                //if this is true, the enemy is attacking.
                blackboard.SetVariable(str, true);
                playerIsDead = true;
            }

            //since this node is only called once the previous node has returned a succes
            //we can do our attack logic here (preferably in an enemymanager script).
            // Debug.Log("attacking");
            if (playerIsDead)
            {
                //if the target is dead, we clear the data and return to patrolling.
                state = NodeState.SUCCES;
                blackboard.ClearData(GuardBT.Settings.TargetStr);
                SceneManager.LoadScene("Dead");
                return state;
            }
            state = NodeState.FAILURE;
            return state;

        }
        state = NodeState.FAILURE;
        return state;
        // Debug.Log(dist);
    }
}
