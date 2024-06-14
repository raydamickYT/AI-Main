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
    private float waitCounter = 0, projectileCounter = 0;
    private float waitTime = 1;
    private bool hasThrown = false;
    private AllyBT allyBT;

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
        object t = GetData(settings.ThrownObjectStr + projectileCounter);
        Vector3 positionOfAgent1 = GlobalBlackboard.Instance.GetAIPosition("EnemyGuard");

        if (startPos == null) startPos = transform;

        GameObject ThrownObject = settings.ThrowObject(startPos, positionOfAgent1, 85);

        if (ThrownObject != null && !hasThrown)
        {
            hasThrown = true;
            GlobalBlackboard.Instance.SetVariable("hasThrown", hasThrown);
            // SetData(settings.ThrownObjectStr +projectileCounter, ThrownObject.transform);
            // projectileCounter++;
            Debug.Log("Projectile thrown" + projectileCounter);
            state = NodeState.SUCCES;
            return state;
        }
        else
        {
            Debug.LogError("Failed to throw projectile");
            state = NodeState.FAILURE;
            return state;
        }
        // if (t == null)
        // {
        // }

        // state = NodeState.FAILURE;
        // return state;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        // Reset waiting state on enter
        waiting = false;
        // hasThrown = false;
        hasThrown = GlobalBlackboard.Instance.GetVariable<bool>("hasThrown");
        Debug.LogWarning("Trowing Object " + hasThrown);
        if (allyBT == null)
        {
            allyBT = transform.GetComponent<AllyBT>();
            allyBT.StateText.text = "TaskThrowProjectile";
        }
        else
        {
            allyBT.StateText.text = "TaskThrowProjectile";
        }
        // Debug.Log("should hide: " + shouldHide);
    }

    public override void OnExit()
    {
        base.OnExit();
        // Reset waiting state on exit
        // Debug.LogWarning("dit is mijn state" + state);
        waiting = false;
        // hasThrown = false;
        waitCounter = 0;
    }
}
