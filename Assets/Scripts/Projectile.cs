using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SphereCollider sp;

    void OnEnable()
    {
        StartCoroutine(ActivateCollision());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ActivateCollision()
    {
        yield return new WaitForSeconds(1);
        sp.enabled = true;
    }

    IEnumerator SmokeBombEffect()
    {
        yield return new WaitForSeconds(3);
        GlobalBlackboard.Instance.SetVariable("EnemyIsBlind", false);
        Destroy(this);
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("rook bom gaat af");
        GlobalBlackboard.Instance.SetVariable("EnemyIsBlind", true);
        StartCoroutine(SmokeBombEffect());
    }
}
