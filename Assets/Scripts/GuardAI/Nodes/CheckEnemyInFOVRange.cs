using BehaviourTree;
using UnityEngine;

public class CheckEnemyInFOVRange : Node
{
    private Transform transform;
    private GuardBT guardBT;

    public CheckEnemyInFOVRange(Transform _transform)
    {
        transform = _transform;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        //tekst aanpassen boven het hoofd van de ai
        if (guardBT == null)
        {
            guardBT = transform.GetComponent<GuardBT>();
            guardBT.StateText.text = "CheckEnemyInFOVRange";
        }
        else
        {
            guardBT.StateText.text = "CheckEnemyInFOVRange";
        }
        // var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
        // GlobalBlackboard.Instance.SetVariable(str, false);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override NodeState Evaluate()
    {
        object t = GetData(GuardBT.Settings.TargetStr);
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GuardBT.Settings.PerceptionRadius, GuardBT.Settings.TargetMask);
            if (colliders.Length > 0)
            {
                //we store the collider  the root in case we collide with an enemy. 
                //the root is 2 levels above, thus the parent.parent
                Parent.Parent.SetData(GuardBT.Settings.TargetStr, colliders[0].transform);

                //laat aan de ally weten dat hij hem heeft gezien en is begonnen met chasen.
                var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
                GlobalBlackboard.Instance.SetVariable(str, true);

                state = NodeState.SUCCES;
                return state;
            }
            //if no collider was found, the node has failed
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCES;
        return state;
    }
}
