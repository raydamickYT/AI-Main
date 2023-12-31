﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus { Success, Failed, Running }
public abstract class BTBaseNode
{

    private bool wasEntered = false;

    protected Blackboard blackboard;

    protected abstract TaskStatus OnUpdate();

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
    public virtual void OnReset() { }

    public TaskStatus Tick()
    {
        if (!wasEntered)
        {
            OnEnter();
            wasEntered = true;
        }

        var result = OnUpdate();
        if(result != TaskStatus.Running)
        {
            OnExit();
            wasEntered = false;
        }
        return result;
    }

    public virtual void SetupBlackboard(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }
}

public abstract class BTComposite : BTBaseNode
{
    protected BTBaseNode[] children;
    public BTComposite (params BTBaseNode[] children)
    {
        this.children = children;
    }

    public override void SetupBlackboard(Blackboard blackboard)
    {
        base.SetupBlackboard(blackboard);
        foreach(BTBaseNode node in children)
        {
            node.SetupBlackboard(blackboard);
        }
    }
}

public abstract class BTDecorator : BTBaseNode
{
    protected BTBaseNode child;
    public BTDecorator(BTBaseNode child)
    {
        this.child = child;
    }

    public override void SetupBlackboard(Blackboard blackboard)
    {
        base.SetupBlackboard(blackboard);
        child.SetupBlackboard(blackboard);
    }
}
