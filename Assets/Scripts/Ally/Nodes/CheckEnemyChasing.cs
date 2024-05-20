using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

namespace BehaviourTree
{

}

public class CheckEnemyChasing : Node
{

    public override NodeState Evaluate()
    {
        bool isChasing = GlobalBlackboard.Instance.GetVariable<bool>(GlobalBlackboard.Instance.IsChasingPlayerStr);
        if (isChasing) //als de enemy niet meer aanvalt dan kan de ally stoppen met hiden (dus dan returned er een succes)
        {
            return NodeState.FAILURE;
        }
        else
        {
            UnityEngine.Debug.Log("Enemy gave up chase");
            return NodeState.SUCCES;
        }

    }
}
