using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEditor;

public class TaskHideFromEnemy : Node
{
    Transform transform;
    public TaskHideFromEnemy(Transform _transform)
    {
        transform = _transform;
    }


    /// <summary>
    /// this script will have the ally move towards the tree and find a face of the tree which is facing furthest away from the enemy.
    /// </summary>
    /// <returns></returns>
    public override NodeState Evaluate()
    {
        Transform tree = (Transform)GetData(AllyBT.Settings.TreeStr);
        Transform enemy = (Transform)GetData(AllyBT.Settings.PlayerTargetStr);

        //calc the dir from the enemy
        Vector3 directionFromEnemyToTree = (tree.position - enemy.position).normalized;

        // calc the pos on the opposite side of the tree
        // 'distanceFromTree' is how far from the tree you want the AI to stop
        float distanceFromTree = 1.0f;
        Vector3 oppositePosition = tree.position + directionFromEnemyToTree * distanceFromTree;


        // hier in de toekomst misschien een bool check doen die checkt of de enemy nog aan het aanvallen is.
        //voor nu gwn code
        float distanceToOppositePosition = Vector3.Distance(transform.position, oppositePosition);
        transform.position = Vector3.MoveTowards(transform.position, oppositePosition, AllyBT.speed(distanceToOppositePosition) * Time.deltaTime);
        if (distanceToOppositePosition < 0.1f)
        {
            state = NodeState.SUCCES;
            return state;
        }


        state = NodeState.RUNNING;
        return state;
    }
}
