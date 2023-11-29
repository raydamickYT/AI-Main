using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForNearbyTree : Node
{
    Transform transform;
    public CheckForNearbyTree(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        object t = (Transform)GetData(AllyBT.Settings.TreeStr);
        if (t == null)
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, AllyBT.Settings.PerceptionRadius, AllyBT.Settings.TreeMask);
            if (collisions.Length > 0)
            {
                Parent.Parent.SetData(AllyBT.Settings.TreeStr, collisions[0].transform);

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
