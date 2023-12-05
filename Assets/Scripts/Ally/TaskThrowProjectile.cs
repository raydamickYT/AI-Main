using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.Animations;


public class TaskThrowProjectile : Node
{
    private Transform transform, startPos;
    private AllySettings settings;

    public TaskThrowProjectile(Transform _transform)
    {
        transform = _transform;
        settings = AllyBT.Settings;
    }


    /// <summary>
    /// dit moet nog aangepast worden. de enemy schiet nu correct de projectile, maar de enemypos is outdated. 
    /// ik denk dat ik bij de enemy een state toegevoegd gaat worden die aangeeft dat de enemy aan aan het vallen is. 
    /// als dat waar is dan checkt de ally nog een keer de pos van de enemy en schiet hij de projectile.
    /// die value die iedereen kan lezen kan bereikt worden met een blackboard.
    /// </summary>
    /// <returns></returns>
    public override NodeState Evaluate()
    {
        object t = GetData(settings.ThrownObject);
        // Transform enemyObject = (Transform)GetData(settings.PlayerTargetStr);
        Vector3 positionOfAgent1 = GlobalBlackboard.Instance.GetAIPosition("EnemyGuard");
        if (t == null)
        {
            if (startPos == null) startPos = transform;
            GameObject ThrownObject = settings.ThrowObject(startPos, positionOfAgent1, 80);
            if (ThrownObject != null)
            {
                Debug.Log(t);
                SetData(settings.ThrownObject, ThrownObject.transform);
                state = NodeState.SUCCES;
                return state;
            }

        }
        state = NodeState.SUCCES;
        return state;
    }
}
