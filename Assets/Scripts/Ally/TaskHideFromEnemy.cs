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
    private Vector3 randomPos, coverPoint;
    Transform enemy;
    private float rangeRandPoint = 6, distToCover = 1;
    private LayerMask obstructionLayer;

    public TaskHideFromEnemy(Transform _transform, NavMeshAgent _nav)
    {
        nav = _nav;
        transform = _transform;
        obstructionLayer = AllyBT.Settings.TreeMask | AllyBT.Settings.EnemyMask | AllyBT.Settings.PlayerMask;
    }


    /// <summary>
    /// this script will have the ally move towards the tree and find a face of the tree which is facing furthest away from the enemy.
    /// </summary>
    /// <returns></returns>
    public override NodeState Evaluate()
    {
        enemy = (Transform)GetData(AllyBT.Settings.PlayerTargetStr);
        Transform coverSpot = (Transform)GetData(AllyBT.Settings.TreeStr);
        Debug.DrawLine(enemy.position, coverSpot.position, Color.red, 2);
        if (coverSpot != null)
        {
            Debug.Log("running to spot");

            nav.SetDestination(coverSpot.position);
        }


        //else its still looking.
        state = NodeState.RUNNING;
        return state;
    }

}
