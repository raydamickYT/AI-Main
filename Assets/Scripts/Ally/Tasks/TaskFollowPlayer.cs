using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.UI;
using UnityEngine.AI;
public class TaskFollowPlayer : Node
{
    public Transform transform, playerTransform;
    private NavMeshAgent nav;

    public override void OnEnter()
    {
        Debug.LogWarning("following player");
        base.OnEnter();
    }

    public TaskFollowPlayer(Transform _transform, Transform _playerTransform, NavMeshAgent _nav)
    {
        nav = _nav;
        transform = _transform;
        playerTransform = _playerTransform;
    }

    public override NodeState Evaluate()
    {
        // Debug.Log("running");
        nav.SetDestination(playerTransform.position);
        state = NodeState.RUNNING;
        return state;
    }
}
