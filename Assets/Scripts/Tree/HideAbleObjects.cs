using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAbleObjects : MonoBehaviour
{
    public Transform[] HidingSpot;
    // Start is called before the first frame update
    void Start()
    {
        if (HidingSpot.Length == 0)
        {
            Debug.LogWarning("No Hiding spot assigned on: " + this.gameObject.name);
        }
    }

}
