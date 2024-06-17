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
    private float projectileCounter = 0;
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
            // Debug.Log("Already thrown, returning SUCCESS");
            state = NodeState.SUCCES;
            return state;
        }

        string str = GlobalBlackboard.Instance.AttackingPlayerStr;
        bool isAllowedToThrow = GlobalBlackboard.Instance.GetVariable<bool>(str);

        // Logic to throw projectile
        // object t = GetData(settings.ThrownObjectStr + projectileCounter);
        object t = blackboard.GetVariable<object>(settings.ThrownObjectStr + projectileCounter);
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
        hasThrown = GlobalBlackboard.Instance.GetVariable<bool>("hasThrown");

        //update tekst boven ai hoofd
        if (allyBT == null)
        {
            allyBT = transform.GetComponent<AllyBT>();
            allyBT.StateText.text = "TaskThrowProjectile";
        }
        else
        {
            allyBT.StateText.text = "TaskThrowProjectile";
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        // Reset waiting state on exit
        // Debug.LogWarning("dit is mijn state" + state);
        // hasThrown = false;
    }
}
