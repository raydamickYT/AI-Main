using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskHideFromEnemy : Node
{
    private Transform transform;
    private NavMeshAgent nav;
    private LayerMask obstructionLayer;

    public TaskHideFromEnemy(Transform _transform, NavMeshAgent _nav)
    {
        nav = _nav;
        transform = _transform;
        obstructionLayer = AllyBT.Settings.TreeMask | AllyBT.Settings.EnemyMask | AllyBT.Settings.PlayerMask;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("TaskHideFromEnemy: OnEnter");
    }

    public override NodeState Evaluate()
    {
        Transform coverSpot = (Transform)GetData(AllyBT.Settings.TreeStr);
        if (coverSpot != null)
        {
            if (!nav.pathPending)
            {
                nav.SetDestination(coverSpot.position);
            }

            float dist = Vector3.Distance(coverSpot.position, transform.position);
            Debug.Log("1Distance to cover spot: " + dist);

            if (dist <= 3) // Changed to match the CheckHidingSpotReached condition
            {
                Debug.Log("1TaskHideFromEnemy: Reached cover spot.");
                state = NodeState.SUCCES;
                return state;
            }

            state = NodeState.RUNNING;
            Debug.Log("1TaskHideFromEnemy: Moving to cover spot.");
            return state;
        }

        Debug.Log("1TaskHideFromEnemy: No cover spot found.");
        state = NodeState.FAILURE;
        return state;
    }
}
