using System.Collections.Generic;
using BehaviourTree;
using UnityEditor;

/// <summary>
/// ordering is important in this setup tree. whatever is defined first will have top priority (so it'll execute over the already running tasks)
/// that's why, in this case, patrolling is last. since we only want it to patrol when all else fails. 
///  this is what gives the priority that you defined when drawing the behaviour tree on paper(if you did that)
/// </summary>
/// <returns></returns>
public class GuardBT : Tree
{
    public UnityEngine.Transform[] WayPoints;
    public static float speed = 2, DetectionRange = 6, AtkRange = 1;
    public bool IsAllowedToTrack = false;
    public static string targetStr = "target";

    protected override Node SetupTree()
    {
        Node Root = IsAllowedToTrack ? new Selector(new List<Node>{
            new Sequence(new List<Node>{
                new CheckEnemyInAttackRange(transform),
                new TaskAttackTarget(transform),
            }),
            new Sequence(new List<Node>{
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, WayPoints),
        }) :
        new TaskPatrol(transform, WayPoints);

        return Root;
    }
}
