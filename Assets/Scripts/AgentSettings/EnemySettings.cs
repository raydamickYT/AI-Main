using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Enemy/Settings")]
public class EnemySettings : ScriptableObject
{
    [Header("Speed & String")]
    // Settings
    public float minSpeed = 1;
    public float maxSpeed = 3;
    [SerializeField]
    private string weaponStr = "WeaponsStr";
    public string WeaponsStr
    {
        get { return weaponStr; }
        private set { weaponStr = value; }
    }
    [SerializeField]
    private string targetStr = "enemyTarget";
    public string TargetStr
    {
        get { return targetStr; }
        private set { targetStr = value; }
    }
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
    [Header("Layer Masks")]
    public LayerMask TargetMask, WeaponMask;
    [Header("Avoidance & Detection")]
    public float PerceptionRadius = 5f, AtkRange = 2f, StopDist = 2, SlowDist = 5;
    public float AvoidanceRadius = 1;

}