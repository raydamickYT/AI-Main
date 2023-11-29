using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
public class TaskFollowPlayer : Node
{
    public Transform transform, playerTransform;

    public TaskFollowPlayer(Transform _transform, Transform _playerTransform)
    {
        transform = _transform;
        playerTransform = _playerTransform;
    }

    public override NodeState Evaluate()
    {
        float distanceToTarget = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToTarget + 3 <= AllyBT.Settings.SlowDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position,
             AllyBT.speed(distanceToTarget) * Time.deltaTime);

            transform.LookAt(Vector3.forward);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, AllyBT.Settings.maxSpeed * Time.deltaTime);
        }

        state = NodeState.RUNNING;
        return state;
    }
}
