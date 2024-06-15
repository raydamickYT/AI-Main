using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckIfBlind : Decorator
{
    private GuardBT guardBT;
    public CheckIfBlind(GuardBT _guard, Node _child) : base(_child)
    {
        guardBT = _guard;
    }
    public override void OnEnter()
    {
        //tekst aanpassen boven het hoofd van de ai
        guardBT.StateText.text = "CheckIfBlind";
        base.OnEnter();
    }

    public override NodeState Evaluate()
    {
        bool IsBlind = GlobalBlackboard.Instance.GetVariable<bool>("EnemyIsBlind");
        if (IsBlind)
        {
            var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
            GlobalBlackboard.Instance.SetVariable(str, false);
            state = NodeState.SUCCES;
            return state;

        }
        else
        {
            Transform t = (Transform)GetData(GuardBT.settings.TargetStr);
            child.SetData(GuardBT.settings.TargetStr, t);

            state = child.Evaluate();  //dus als de rook bom niet actief is kan de enemy gewoon zien
            if (state == NodeState.SUCCES)
            {
                //dit doe ik om te vorkomen dat de volgende task wordt uitgevoerd. anders staat de ai stil nadat hij zn wapen oppakt
                state = NodeState.FAILURE;
            }

            return state;
        }
    }
    public override void SetupBlackboard(Blackboard blackboard)
    {
        base.SetupBlackboard(blackboard);
        child.SetupBlackboard(blackboard);
    }
}


