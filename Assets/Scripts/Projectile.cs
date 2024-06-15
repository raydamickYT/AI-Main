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
        yield return new WaitForSeconds(10); //als failsafe. stel dat de bom niks raakt dan kan de ally niet meer gooien.
        Destroy(this);
    }

    IEnumerator SmokeBombEffect(int time)
    {
        Debug.Log("isblind Collision");
        GlobalBlackboard.Instance.SetVariable("EnemyIsBlind", true);
        yield return new WaitForSeconds(time);
        GlobalBlackboard.Instance.SetVariable("EnemyIsBlind", false);
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("rook bom gaat af");
            StartCoroutine(SmokeBombEffect(3));
        }
    }
    void OnDestroy()
    {
        GlobalBlackboard.Instance.SetVariable("hasThrown", false);
    }
}
