using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllySettings", menuName = "Ally/Settings")]
public class AllySettings : ScriptableObject
{
    [Header("Movement and Strings")]
    // Settings
    public float MaxSpeed = 5;
    [SerializeField]
    private string thrownObjectStr = "thrownObject";
    public string ThrownObjectStr
    {
        get { return thrownObjectStr; }
        private set { thrownObjectStr = value; }
    }
    [SerializeField]
    private string playerTarget = "playerTarget";

    public string PlayerTargetStr
    {
        get { return playerTarget; }
        private set { playerTarget = value; }
    }
    [SerializeField]
    private string treeStr = "TreeStr";

    public string TreeStr
    {
        get { return treeStr; }
        private set { treeStr = value; }
    }


    [Header("Avoidance & Detection")]
    public LayerMask EnemyMask, TreeMask, PlayerMask;
    public float PerceptionRadius = 100f, DangerPerceptionRadius = 10f, StopDist = 2, SlowDist = 5;
    public float AvoidanceRadius = 1;


    [Header("Projectile Throwing")]
    [Range(0, 50f)]
    public float ThrowForce;
    public GameObject ObjectToThrow;

    [System.Serializable]
    public struct ProjectileProperties
    {
        public Vector3 Direction, InitialPos;
        public float InitialSpeed, Mass, Drag;
    }
    [Header("Projectile Properties")]
    public ProjectileProperties projectileProperties;

public GameObject ThrowObject(Transform startPos, Vector3 targetPos, float maxThrowAngle = 45.0f)
{
    GameObject thrownObject = Instantiate(ObjectToThrow, startPos.position, Quaternion.identity);
    Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

    Vector3 toTarget = targetPos - startPos.position;

    // Calculate the vertical and horizontal distances
    float yOffset = toTarget.y;
    toTarget.y = 0;
    float distance = toTarget.magnitude;

    float angle = Mathf.Min(maxThrowAngle, Vector3.Angle(Vector3.forward, toTarget.normalized));
    float radianAngle = angle * Mathf.Deg2Rad;

    // Calculate initial speeds for x-z and y
    float initialVelocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radianAngle));
    Vector3 velocityXZ = toTarget.normalized * Mathf.Cos(radianAngle) * initialVelocity;
    float velocityY = Mathf.Sin(radianAngle) * initialVelocity;

    // Adjust for height difference
    float additionalHeight = Mathf.Tan(radianAngle) * distance;
    if (additionalHeight < -yOffset)
    {
        angle = 90 - angle;
        radianAngle = angle * Mathf.Deg2Rad;
        initialVelocity = Mathf.Sqrt(-distance * Physics.gravity.magnitude / Mathf.Sin(2 * radianAngle));
        velocityXZ = toTarget.normalized * Mathf.Cos(radianAngle) * initialVelocity;
        velocityY = Mathf.Sin(radianAngle) * initialVelocity;
    }
    
    // Combine the velocities
    Vector3 finalVelocity = new Vector3(velocityXZ.x, velocityY, velocityXZ.z);

    // Apply the calculated velocity
    rb.velocity = finalVelocity;

    return thrownObject;
}





}