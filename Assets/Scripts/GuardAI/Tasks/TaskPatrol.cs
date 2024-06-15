using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using System.Transactions;
using UnityEngine.AI;
public class TaskPatrol : Node
{
    private Transform transform;
    private NavMeshAgent nav;
    private Transform[] wayPoints;

    private int currentWayPointIndex = 0;

    private float waitTime = 1f; //in seconds
    private float waitCounter = 0f;
    private bool waiting = false;
    private GuardBT guard;
    public TaskPatrol(Transform _transform, Transform[] _wayPoints, NavMeshAgent _nav)
    {
        transform = _transform;
        wayPoints = _wayPoints;
        nav = _nav;
    }
    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                waiting = false;
            }
        }
        else
        {
            Transform wp = wayPoints[currentWayPointIndex];
            float distanceToWayPoint = Vector3.Distance(transform.position, wp.position);
            if (distanceToWayPoint < 1.3f)
            {
                // transform.position = wp.position;
                waitCounter = 0;
                waiting = true;

                currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
            }
            else
            {
                nav.SetDestination(wp.transform.position);
                // transform.LookAt(wp.position);
            }

        }
        state = NodeState.RUNNING;
        return state;
    }

    public override void OnEnter()
    {
        // GlobalBlackboard.Instance.SetVariable("ShouldHide", true); //als de enemy begint met zn achtervolging is dit true
        //we zetten deze var weer op true in: TaskGoToTarget.cs
        if (guard == null)
        {
            guard = transform.GetComponent<GuardBT>();
            guard.StateText.text = "TaskPatrol";
        }
        else
        {
            guard.StateText.text = "TaskPatrol";
        }
        base.OnEnter();
    }
}
