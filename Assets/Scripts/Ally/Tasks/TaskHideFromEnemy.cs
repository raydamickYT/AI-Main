using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEditor;
using UnityEngine.AI;

public class TaskHideFromEnemy : Node
{
    Transform transform;
    private NavMeshAgent nav;
    Transform enemy;
    private LayerMask obstructionLayer;

    public TaskHideFromEnemy(Transform _transform, NavMeshAgent _nav)
    {
        nav = _nav;
        transform = _transform;
        obstructionLayer = AllyBT.Settings.TreeMask | AllyBT.Settings.EnemyMask | AllyBT.Settings.PlayerMask;
    }

    public override void OnEnter()
    {
        // GlobalBlackboard.Instance.SetVariable("ShouldHide", true);
        base.OnEnter();
    }


    /// <summary>
    /// this script will have the ally move towards the tree and find a face of the tree which is facing furthest away from the enemy.
    /// </summary>
    /// <returns></returns>
    public override NodeState Evaluate()
    {
        Transform coverSpot = (Transform)GetData(AllyBT.Settings.TreeStr);
        if (coverSpot != null)
        {

            nav.SetDestination(coverSpot.position);
            float dist = Vector3.Distance(coverSpot.position, transform.position);
            if (dist <= 10)
            {
                // Debug.Log("end reached");
                state = NodeState.SUCCES;
                return state;
            }
            state = NodeState.RUNNING;
            return state;
        }

        //else its still looking.
        state = NodeState.FAILURE;
        // Debug.Log("State " + state);
        return state;
    }

}
