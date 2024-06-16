using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Enemy/Settings")]
public class EnemySettings : ScriptableObject
{
    [Header("Speed & String")]
    // Settings
    public float MinSpeed = 1;
    public float MaxSpeed = 3;
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
    public float PatrollingSpeed(float dist)
    {
        float proportionalDistance = dist / SlowDist;
        float speed = UnityEngine.Mathf.Lerp(MinSpeed, MaxSpeed, proportionalDistance);

        return speed;
    }
    public float PersueSpeed(float dist)
    {
        float proportionalDistance = (dist - StopDist) / (SlowDist - StopDist);
        float speed = UnityEngine.Mathf.Lerp(MinSpeed, MaxSpeed, proportionalDistance);

        return speed;
    }
    [Header("Layer Masks")]
    public LayerMask TargetMask, WeaponMask;
    [Header("Avoidance & Detection")]
    public float PerceptionRadius = 5f, AtkRange = 2f, StopDist = 2, SlowDist = 5;
}