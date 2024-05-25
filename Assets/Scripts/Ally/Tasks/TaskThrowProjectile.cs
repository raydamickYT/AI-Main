using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskThrowProjectile : Node
{
    private Transform transform;
    private Transform startPos;
    private AllySettings settings;
    private bool waiting = false;
    private float waitCounter = 0;
    private float waitTime = 1;
    private bool hasThrown = false;

    public TaskThrowProjectile(Transform _transform)
    {
        transform = _transform;
        settings = AllyBT.Settings;
    }
    public override NodeState Evaluate()
    {
        //check of hij niet al gegooid heeft
        if (hasThrown)
        {
            Debug.Log("Already thrown, returning SUCCESS");
            state = NodeState.SUCCES;
            return state;
        }

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
                Debug.Log("Projectile thrown");
                state = NodeState.SUCCES;
                hasThrown = true;
                return state;
            }
            else
            {
                Debug.LogError("Failed to throw projectile");
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.FAILURE;
        return state;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        // Reset waiting state on enter
        waiting = false;
        hasThrown = false;
        Debug.LogWarning("Trowing Object");
        string str = GlobalBlackboard.Instance.IsChasingPlayerStr;
        bool shouldHide = GlobalBlackboard.Instance.GetVariable<bool>(str);
        // Debug.Log("should hide: " + shouldHide);
    }

    public override void OnExit()
    {
        base.OnExit();
        // Reset waiting state on exit
        // Debug.LogWarning("dit is mijn state" + state);
        waiting = false;
        hasThrown = false;
    }
}
