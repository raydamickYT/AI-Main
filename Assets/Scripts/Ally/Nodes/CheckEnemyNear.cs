using UnityEngine;
using BehaviourTree;
public class CheckEnemyNear : Node
{

    private Transform transform;

    public CheckEnemyNear(Transform _transform, LayerMask _enemyMask)
    {
        transform = _transform;
    }

    public override void OnEnter()
    {
        base.OnEnter(); 
        Debug.LogWarning("CheckenemyNear");
    }
    //checks if there is an enemy near and adds its transform to a list for future use.
    public override NodeState Evaluate()
    {
        Vector3 enemyPosition = GlobalBlackboard.Instance.GetAIPosition("EnemyGuard");

        // Check if enemy position is valid (not Vector3.zero, or use a different method to validate)
        if (enemyPosition != Vector3.zero)
        {
            float dist = Vector3.Distance(transform.position, enemyPosition);

            if (dist < AllyBT.Settings.DangerPerceptionRadius)
            {
                // Enemy is within perception radius
                Debug.Log("Parent" + Parent);
                Parent.Parent.SetData(AllyBT.Settings.PlayerTargetStr, enemyPosition);
                state = NodeState.SUCCES;
            }
            else
            {
                // Enemy is outside perception radius
                state = NodeState.FAILURE;
            }
        }
        else
        {
            // No valid enemy position available
            state = NodeState.FAILURE;
        }

        return state;
    }


}
