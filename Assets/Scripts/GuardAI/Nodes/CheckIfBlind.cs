using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckIfBlind : Node
{
    public CheckIfBlind()
    {

    }
    public override NodeState Evaluate()
    {
        bool IsBlind = GlobalBlackboard.Instance.GetVariable<bool>("EnemyIsBlind");
        if (IsBlind)
        {
            state = NodeState.FAILURE; 
            return state;
        }
        else
        {
            state = NodeState.SUCCES;  //dus als de rook bom niet actief is kan de enemy gewoon zien
            return state;
        }
    }
}
