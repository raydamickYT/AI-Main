using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class CheckEnemyNear : Node
{
    //7th layer (moves 1 bit 7 places to the left)
    private LayerMask enemyLayerMask;
    private Transform transform;
    public CheckEnemyNear(Transform _transform, LayerMask _enemyMask)
    {
        transform = _transform;
        enemyLayerMask = _enemyMask;
    }


    //checks if there is an enemy near and adds its transform to a list for future use.
    public override NodeState Evaluate()
    {
        object t = GetData(AllyBT.Settings.PlayerTargetStr);
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, AllyBT.Settings.DangerPerceptionRadius, enemyLayerMask);
            if (colliders.Length > 0)
            {
                Parent.Parent.SetData(AllyBT.Settings.PlayerTargetStr, colliders[0].transform);

                state = NodeState.SUCCES;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCES;
        return state;
    }
}
