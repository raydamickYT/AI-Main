using BehaviourTree;
using UnityEngine;

public class CheckEnemyInAttackRange : Node
{
    private static int enemyLayerMask = 1 << 6;
    private Transform transform;

    public CheckEnemyInAttackRange(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData(GuardBT.targetStr);
        if (t == null)
        {
            //if no collider was found, the node has failed
            state = NodeState.FAILURE;
            return state;
        }

        //no need for a 2nd collision detection, because we already have the target pos in the dictionary, since it's been found (not null)
        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) <= GuardBT.AtkRange)
        {
            state = NodeState.SUCCES;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
