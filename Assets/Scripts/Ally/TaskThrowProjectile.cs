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
    private bool waiting = false;
    private float waitCounter = 0;
    private float waitTime = 1;

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
    string str = GlobalBlackboard.Instance.AttackingPlayerStr;
    bool isAllowedToThrow = GlobalBlackboard.Instance.GetVariable<bool>(str);

    // Start the waiting timer if allowed to throw and not already waiting
    if (isAllowedToThrow && !waiting)
    {
        waiting = true;
        waitCounter = 0;
    }

    // While waiting, increment the counter
    if (waiting)
    {
        waitCounter += Time.deltaTime;
        if (waitCounter < waitTime)
        {
            // Still waiting, so return RUNNING
            state = NodeState.RUNNING;
            return state;
        }

        // Wait time completed, proceed to throw the projectile
        waiting = false;
    }

    // Logic to throw projectile
    object t = GetData(settings.ThrownObjectStr);
    Vector3 positionOfAgent1 = GlobalBlackboard.Instance.GetAIPosition("EnemyGuard");

    if (t == null)
    {
        if (startPos == null) startPos = transform;

        GameObject ThrownObject = settings.ThrowObject(startPos, positionOfAgent1, 85);

        if (ThrownObject != null)
        {
            SetData(settings.ThrownObjectStr, ThrownObject.transform);
            Debug.Log("thrown");
            state = NodeState.SUCCES;
            return state;
        }
    }

    state = NodeState.RUNNING;
    return state;
}

}
