using System;
using System.Collections.Generic;
using BehaviourTree;
using UnityEditor;
using UnityEngine.AI;

/// <summary>
/// ordering is important in this setup tree. whatever is defined first will have top priority (so it'll execute over the already running tasks)
/// that's why, in this case, patrolling is last. since we only want it to patrol when all else fails. 
///  this is what gives the priority that you defined when drawing the behaviour tree on paper(if you did that)
/// </summary>
/// <returns></returns>
public class GuardBT : Tree
{
    public EnemySettings _settings;
    public static EnemySettings settings;
    public UnityEngine.GameObject WeaponHolder;
    public UnityEngine.Transform[] WayPoints;
    // [UnityEngine.HideInInspector]
    public List<UnityEngine.GameObject> EquippedItems = new();
    public bool IsAllowedToTrack = false;
    public bool InNeedOfWeapon = true;
    public NavMeshAgent nav;

    protected override Node SetupTree()
    {
        Node Root = IsAllowedToTrack ?
            new Selector(new List<Node>{
        new Sequence(new List<Node>{
            new CheckEnemyInFOVRange(transform),
            new Selector(new List<Node>{
                new Sequence(new List<Node>{
                    new CheckEnemyInAttackRange(transform,this),
                    new TaskAttackTarget(transform),
                }),
                new Sequence(new List<Node>{
                    new CheckIfWeaponInInventory(this),
                    new TaskPickUpWeapon(transform, nav, this),
                }),
                new TaskGoToTarget(transform, nav, this),
            })
        }),
        new TaskPatrol(transform, WayPoints, nav),
            }) :
            new TaskPatrol(transform, WayPoints, nav);


        return Root;
    }

    protected override void Initialization()
    {
        string str = GlobalBlackboard.Instance.AttackingPlayerStr;

        GlobalBlackboard.Instance.SetVariable(str, false);
        if (_settings == null)
        {
            UnityEngine.Debug.LogWarning("EnemySettings.cs is not assigned in GuardBT");
        }
        else
        {
            settings = _settings;
        }
    }
}
