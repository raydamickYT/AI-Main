using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskHideFromEnemy : Node
{
    private Transform transform;
    private NavMeshAgent nav;
    private AllyBT allyBT;

    public TaskHideFromEnemy(Transform _transform, NavMeshAgent _nav)
    {
        nav = _nav;
        transform = _transform;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        //update tekst boven ai hoofd
        if (allyBT == null)
        {
            allyBT = transform.GetComponent<AllyBT>();
            allyBT.StateText.text = "TaskHideFromEnemy";
        }
        else
        {
            allyBT.StateText.text = "TaskHideFromEnemy";
        }
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

            if (dist <= 5) // is hij in de buurt?
            {
                state = NodeState.SUCCES;
                return state;
            }

            state = NodeState.RUNNING;
            return state;
        }

        // Debug.Log("1TaskHideFromEnemy: No cover spot found.");
        state = NodeState.FAILURE;
        return state;
    }
}
