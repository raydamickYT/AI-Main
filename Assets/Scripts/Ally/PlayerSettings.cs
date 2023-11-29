using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllySettings", menuName = "Ally/Settings")]
public class AllySettings : ScriptableObject
{
    // Settings
    public float minSpeed = 2;
    public float maxSpeed = 5;
    public string PlayerTargetStr = "playerTarget";
    public string TreeStr = "TreeStr";
    public float speed(float dist)
    {
        float proportionalDistance = (dist - StopDist) / (SlowDist - StopDist);
        float speed = UnityEngine.Mathf.Lerp(0, maxSpeed, proportionalDistance);

        return speed;
    }

    [Header("Avoidance & Detection")]
    public LayerMask EnemyMask, TreeMask;
    public float PerceptionRadius = 100f, DangerPerceptionRadius = 10f, StopDist = 2, SlowDist = 5;
    public float AvoidanceRadius = 1;

}