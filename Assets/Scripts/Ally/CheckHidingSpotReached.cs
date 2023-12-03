using BehaviourTree;
using UnityEngine;

public class CheckHidingSpotReached : Node
{
    public Transform transform;
    public CheckHidingSpotReached(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        //if the ally is not near the hiding spot this'll return a false
        Transform coverSpot = (Transform)GetData(AllyBT.Settings.TreeStr);
        if (coverSpot != null)
        {
            float dist = Vector3.Distance(transform.position, coverSpot.position);
            if (dist < 3)
            {
                state = NodeState.SUCCES;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
