using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using System.Transactions;
public class TaskPatrol : Node
{
    private Transform transform;
    private Transform[] wayPoints;

    private int currentWayPointIndex = 0;

    private float waitTime = 1f; //in seconds
    private float waitCounter = 0f;
    private bool waiting = false;
    public TaskPatrol(Transform _transform, Transform[] _wayPoints)
    {
        transform = _transform;
        wayPoints = _wayPoints;
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

            if (distanceToWayPoint < 0.5f)
            {
                // transform.position = wp.position;
                waitCounter = 0;
                waiting = true;

                currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, GuardBT.settings.patrollingSpeed(distanceToWayPoint) * Time.deltaTime);
                transform.LookAt(wp.position);
            }

        }
        state = NodeState.RUNNING;
        return state;
    }
}
