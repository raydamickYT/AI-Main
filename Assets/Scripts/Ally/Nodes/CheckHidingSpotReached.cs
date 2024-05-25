using BehaviourTree;
using UnityEngine;

public class CheckHidingSpotReached : Node
{
    public Transform transform;
    public CheckHidingSpotReached(Transform _transform)
    {
        transform = _transform;
    }
    public override void OnEnter()
    {
        Debug.LogWarning("Checking if I'm near a hiding spot");
    }
    public override NodeState Evaluate()
    {
        //if the ally is not near the hiding spot this'll return a false
        Transform coverSpot = (Transform)GetData(AllyBT.Settings.TreeStr);
        string str = GlobalBlackboard.Instance.IsChasingPlayerStr;
        bool shouldHide = GlobalBlackboard.Instance.GetVariable<bool>(str);
        Debug.Log("should hide: "+ shouldHide);

        if (coverSpot != null)
        {
            float dist = Vector3.Distance(transform.position, coverSpot.position);
            if (dist < 3)
            {
                state = NodeState.SUCCES;
                // GlobalBlackboard.Instance.SetVariable("ShouldHide", false);
                Debug.Log("state2: " + state);
                return state;
            }
            state = NodeState.RUNNING;
            Debug.Log("state: " + state);

            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
