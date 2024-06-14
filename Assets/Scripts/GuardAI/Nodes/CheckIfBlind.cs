using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckIfBlind : Decorator
{
    public CheckIfBlind(Node _child) : base(_child)
    {
    }

    public override NodeState Evaluate()
    {
        bool IsBlind = GlobalBlackboard.Instance.GetVariable<bool>("EnemyIsBlind");
        bool? isBlind = GlobalBlackboard.Instance.GetVariable<bool?>("EnemyIsBlind");
        Debug.Log("isblind " + IsBlind);
        if (IsBlind)
        {
            state = NodeState.FAILURE;
            return state;
        }
        else
        {
            var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
            // GlobalBlackboard.Instance.SetVariable(str, false);
            Transform t = (Transform)GetData(GuardBT.settings.TargetStr);
            child.SetData(GuardBT.settings.TargetStr, t);
            state = child.Evaluate();  //dus als de rook bom niet actief is kan de enemy gewoon zien
            return state;
        }
    }
    public override void SetupBlackboard(Blackboard blackboard)
    {
        base.SetupBlackboard(blackboard);
        child.SetupBlackboard(blackboard);
    }

}


