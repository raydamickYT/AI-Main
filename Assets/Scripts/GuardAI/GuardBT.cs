using System.Collections.Generic;
using BehaviourTree;
using UnityEditor;

/// <summary>
/// ordering is important in this setup tree. 
///  this is what gives the priority that you defined when drawing the behaviour tree on paper(if you did that)
/// </summary>
/// <returns></returns>
public class GuardBT : Tree
{
    public UnityEngine.Transform[] WayPoints;
    public static float speed = 2, DetectionRange = 6, AtkRange = 1;
    public static string target = "target";

    protected override Node SetupTree()
    {
        Node Root = new Selector(new List<Node>{
            new Sequence(new List<Node>{
                new CheckEnemyInAttackRange(transform),
                new TaskAttackTarget(transform),
            }),
            new Sequence(new List<Node>{
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, WayPoints),
        });

        return Root;
    }
}
