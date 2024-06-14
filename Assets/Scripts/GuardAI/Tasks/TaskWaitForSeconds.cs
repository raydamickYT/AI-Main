using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskWaitForSeconds : Node
{
    private float duration;
    private float startTime;

    public TaskWaitForSeconds(float duration)
    {
        this.duration = duration;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
    }

    public override NodeState Evaluate()
    {
        if (Time.time - startTime > duration)
        {
            state = NodeState.SUCCES;
        }
        else
        {
            state = NodeState.RUNNING;
        }
        return state;
    }
}
