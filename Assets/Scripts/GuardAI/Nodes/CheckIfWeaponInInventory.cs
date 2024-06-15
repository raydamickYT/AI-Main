using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckIfWeaponInInventory : Node
{
    private GuardBT guard;
    public CheckIfWeaponInInventory(GuardBT _guard)
    {
        guard = _guard;
    }
    public override void OnEnter()
    {

        guard.StateText.text = "CheckIfWeaponInInventory";
        base.OnEnter();
    }

    public override NodeState Evaluate()
    {
        if (guard.EquippedItems.Count == 0)
        {
            guard.InNeedOfWeapon = true;
        }
        else
        {
            guard.InNeedOfWeapon = false;
        }

        //kan nog andere dingen toevoegen, zoals: check voor ammo
        state = guard.InNeedOfWeapon ? NodeState.SUCCES : NodeState.FAILURE;
        return state;
    }
}
