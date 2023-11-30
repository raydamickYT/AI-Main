using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForNearbyTree : Node
{
    Transform transform;
    private LayerMask obstructionLayer;

    public CheckForNearbyTree(Transform _transform)
    {
        transform = _transform;
        obstructionLayer = AllyBT.Settings.TreeMask | AllyBT.Settings.EnemyMask;
    }

    public override NodeState Evaluate()
    {
        object t = (Transform)GetData(AllyBT.Settings.TreeStr);
        if (t == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, AllyBT.Settings.PerceptionRadius, AllyBT.Settings.TreeMask);
            if (hitColliders.Length > 0)
            {
                Transform bestCoverSpot = FindBestCoverSpot(hitColliders);

                if (bestCoverSpot != null)
                {
                    Parent.Parent.SetData(AllyBT.Settings.TreeStr, bestCoverSpot);
                    // nav.SetDestination(bestCoverSpot.position);
                    state = NodeState.SUCCES;
                    return state;
                }
                else
                {
                    Debug.Log("No effective cover found");
                    state = NodeState.FAILURE;
                    return state;
                }
            }
        }

        state = NodeState.SUCCES;
        return state;
    }

    Transform FindBestCoverSpot(Collider[] colliders)
    {
        Transform bestCover = null;
        float bestCoverRating = 0f;
        foreach (Collider potentialCover in colliders)
        {
            Vector3 coverPosition = potentialCover.transform.position;
            float coverRating = RateCoverSpot(coverPosition);
            Debug.Log(coverRating);

            if (coverRating > bestCoverRating)
            {
                bestCover = potentialCover.transform;
                bestCoverRating = coverRating;
            }
        }

        return bestCover;
    }

    float RateCoverSpot(Vector3 coverPosition)
    {
        // A simple rating method - can be more complex based on game requirements
        float rating = 0f;
        Transform enemy = (Transform)GetData(AllyBT.Settings.PlayerTargetStr);

        // Check if this cover spot effectively blocks the line of sight from the enemy
        if (Physics.Linecast(enemy.position, coverPosition, obstructionLayer))
        {
            // Increase rating if line of sight is blocked
            rating += 1f;
        }

        // Additional rating criteria can be added here

        return rating;
    }
}
