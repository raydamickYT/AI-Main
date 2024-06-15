using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TaskPickUpWeapon : Node
{
    private Transform transform;
    private NavMeshAgent nav;
    private GuardBT guard;

    public TaskPickUpWeapon(Transform _transform, NavMeshAgent _nav, GuardBT _guard)
    {
        transform = _transform;
        nav = _nav;
        guard = _guard;
    }

    public override void OnEnter()
    {
        guard.StateText.text = "TaskPickUpWeapon";
        base.OnEnter();
    }

    public override NodeState Evaluate()
    {
        object w = GetData(GuardBT.settings.WeaponsStr);
        if (guard.EquippedItems.Count < 1)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 100, GuardBT.settings.WeaponMask);
            // Debug.Log(colliders.Length);
            if (colliders.Length > 0)
            {
                SetData(GuardBT.settings.WeaponsStr, colliders[0]);
                GameObject item = colliders[0].gameObject;
                float dist = Vector3.Distance(transform.position, item.transform.position);
                // transform.position = Vector3.MoveTowards(transform.position, item.transform.position, GuardBT.settings.persueSpeed(dist) * Time.deltaTime);
                nav.SetDestination(item.transform.position);
                transform.LookAt(Vector3.forward);
                if (dist < 1.2)
                {
                    // item.SetActive(false);
                    guard.EquippedItems.Add(item);
                    item.transform.SetParent(guard.WeaponHolder.transform);

                    //reset local values
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;

                    //set local scale
                    item.transform.localScale = Vector3.one;

                    //set world pos
                    // item.transform.position = guard.WeaponHolder.transform.position;

                    //return succes
                    state = NodeState.SUCCES;
                    return state;
                }
                state = NodeState.RUNNING;
                return state;
            }
        }
        state = NodeState.SUCCES;
        return state;
    }
}
