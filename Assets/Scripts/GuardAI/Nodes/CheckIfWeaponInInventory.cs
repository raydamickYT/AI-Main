using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckIfWeaponInInventory : Node
{
    private GuardBT guardBT;
    public CheckIfWeaponInInventory(GuardBT _guard)
    {
        guardBT = _guard;
    }
    public override void OnEnter()
    {

        guardBT.StateText.text = "CheckIfWeaponInInventory";
        base.OnEnter();
    }

    public override NodeState Evaluate()
    {
        if (guardBT.EquippedItems.Count == 0)
        {
            guardBT.InNeedOfWeapon = true;
        }
        else
        {
            guardBT.InNeedOfWeapon = false;
        }

        //kan nog andere dingen toevoegen, zoals: check voor ammo
        state = guardBT.InNeedOfWeapon ? NodeState.SUCCES : NodeState.FAILURE;
        return state;
    }
}
