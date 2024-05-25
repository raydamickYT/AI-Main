using BehaviourTree;
using UnityEngine;

public class CheckEnemyInFOVRange : Node
{
    private Transform transform;

    public CheckEnemyInFOVRange(Transform _transform)
    {
        transform = _transform;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
        GlobalBlackboard.Instance.SetVariable(str, false);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override NodeState Evaluate()
    {
        object t = GetData(GuardBT.settings.TargetStr);
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GuardBT.settings.PerceptionRadius, GuardBT.settings.TargetMask);
            if (colliders.Length > 0)
            {
                //we store the collider  the root in case we collide with an enemy. 
                //the root is 2 levels above, thus the parent.parent
                Parent.Parent.SetData(GuardBT.settings.TargetStr, colliders[0].transform);
                
                var str = GlobalBlackboard.Instance.IsChasingPlayerStr;
                GlobalBlackboard.Instance.SetVariable(str, true); //als we deze node verlaten zijn we altijd aan het chasen.

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
