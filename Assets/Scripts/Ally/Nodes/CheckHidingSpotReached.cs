using BehaviourTree;
using UnityEngine;

public class CheckHidingSpotReached : Node
{
    public Transform transform;
    private AllyBT allyBT;

    public CheckHidingSpotReached(Transform _transform)
    {
        transform = _transform;
    }
    public override void OnEnter()
    {
        //update tekst boven ai hoofd
        if (allyBT == null)
        {
            allyBT = transform.GetComponent<AllyBT>();
            allyBT.StateText.text = "CheckHidingSpotReached";
        }
        else
        {
            allyBT.StateText.text = "CheckHidingSpotReached";
        }
    }
    public override NodeState Evaluate()
    {
        //if the ally is not near the hiding spot this'll return a false
        // Transform coverSpot = (Transform)GetData(AllyBT.Settings.TreeStr);
        Transform coverSpot = blackboard.GetVariable<Transform>(AllyBT.Settings.TreeStr);

        if (coverSpot != null)
        {
            float dist = Vector3.Distance(transform.position, coverSpot.position);
            if (dist < 3)
            {
                state = NodeState.SUCCES;
                // GlobalBlackboard.Instance.SetVariable("ShouldHide", false);
                // Debug.Log("state2: " + state);
                return state;
            }
            state = NodeState.RUNNING;
            Debug.Log("Hiding: " + state);

            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
