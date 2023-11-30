using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Enemy/Settings")]
public class EnemySettings : ScriptableObject
{
    // Settings
    public float minSpeed = 1;
    public float maxSpeed = 3;
    public string targetStr = "enemyTarget";
    public float patrollingSpeed(float dist)
    {
        float proportionalDistance = dist / SlowDist;
        float speed = UnityEngine.Mathf.Lerp(minSpeed, maxSpeed, proportionalDistance);

        return speed;
    }
    public float persueSpeed(float dist)
    {
        float proportionalDistance = (dist - StopDist) / (SlowDist - StopDist);
        float speed = UnityEngine.Mathf.Lerp(minSpeed, maxSpeed, proportionalDistance);

        return speed;
    }

    [Header("Avoidance & Detection")]
    public LayerMask TargetMask;
    public float PerceptionRadius = 6f, AtkRange = 2f, StopDist = 2, SlowDist = 5;
    public float AvoidanceRadius = 1;

}