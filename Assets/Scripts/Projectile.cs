using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SphereCollider sp;

    void OnEnable()
    {
        if (sp == null)
        {
            sp = GetComponent<SphereCollider>();
        }
        StartCoroutine(ActivateCollision());
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
