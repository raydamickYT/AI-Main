using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForNearbyTree : Node
{
    Transform transform;
    private LayerMask obstructionLayer;
    private AllyBT allyBT;

    public CheckForNearbyTree(Transform _transform)
    {
        transform = _transform;
        obstructionLayer = AllyBT.Settings.TreeMask | AllyBT.Settings.EnemyMask;
    }
    public override void OnEnter()
    {
        //update tekst boven ai hoofd
        if (allyBT == null)
        {
            allyBT = transform.GetComponent<AllyBT>();
            allyBT.StateText.text = "CheckForNearbyTree";
        }
        else
        {
            allyBT.StateText.text = "CheckForearbyTree";
        }
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
                    // Debug.Log("No effective cover found");
                    state = NodeState.FAILURE;
                    return state;
                }

            }
            state = NodeState.FAILURE; //want als er niks is om achter te verschuilen dan is er iets fout in het level.
            return state;
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

            Vector3 directionFromCoverToAI = (transform.position - coverPosition).normalized;
            float offsetDistance = -5f; // offset; adjust as needed

            Vector3 raycastStartPosition = coverPosition + directionFromCoverToAI * offsetDistance;
            float coverRating = RateCoverSpot(raycastStartPosition);
            GameObject coverPositionObject = new GameObject("raycastStartPosition");
            coverPositionObject.transform.position = raycastStartPosition;

            if (coverRating > bestCoverRating)
            {
                bestCover = coverPositionObject.transform;
                bestCoverRating = coverRating;
            }

        }

        return bestCover;
    }

    float RateCoverSpot(Vector3 coverPosition)
    {
        float rating = 0f;
        Vector3 postionOfEnemy = GlobalBlackboard.Instance.GetAIPosition("EnemyGuard");

        // Offset the start position of the raycast slightly towards the AI
        Debug.DrawLine(postionOfEnemy, coverPosition, Color.red, 5);

        // Check if this cover spot blocks the line of sight from the enemy
        if (Physics.Linecast(postionOfEnemy, coverPosition, obstructionLayer))
        {
            // Increase rating if line of sight is blocked
            rating += 1f;
        }

        return rating;
    }
}
