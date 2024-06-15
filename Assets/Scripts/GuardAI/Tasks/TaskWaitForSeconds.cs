using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskWaitForSeconds : Node
{
    private float duration;
    private float startTime;
    private GuardBT guardBT;

    public TaskWaitForSeconds(GuardBT _guardBT, float duration)
    {
        this.duration = duration;
        guardBT = _guardBT;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
    }

    public override NodeState Evaluate()
    {
        float elapsedTime = Time.time - startTime;
        float remainingTime = Mathf.Max(0, duration - elapsedTime);
        guardBT.StateText.text = "TaskWaitforSeconds: " + remainingTime.ToString("F2");
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
