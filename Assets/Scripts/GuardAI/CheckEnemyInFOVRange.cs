using BehaviourTree;
using UnityEngine;

public class CheckEnemyInFOVRange : Node
{
    private static int enemyLayerMask = 1 << 6;
    private Transform transform;

    public CheckEnemyInFOVRange(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData(GuardBT.target);
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GuardBT.DetectionRange, enemyLayerMask);
            if (colliders.Length > 0)
            {
                //we store the collider  the root in case we collide with an enemy. 
                //the root is 2 levels above, thus the parent.parent
                Parent.Parent.SetData(GuardBT.target, colliders[0].transform);

                state = NodeState.SUCCES;
                return state;
            }
            //if no collider was found, the node has failed
            Debug.Log("no enemy in sight");
            state = NodeState.FAILURE;
            return state;
        }

        Debug.Log("enemy found");
        state = NodeState.SUCCES;
        return state;
    }
}
