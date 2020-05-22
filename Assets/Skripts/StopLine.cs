using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLine : MonoBehaviour
{
    public Transform stopObject;
    public float checkRadius = .2f;

    void OnDrawGizmosSelected()
    {
        if (stopObject == null) return;
        Gizmos.DrawWireSphere(stopObject.position, checkRadius);
    }
}
