using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeeTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform head;
    [SerializeField] private LayerMask layerObstacle;

    public UnityEvent<float,Transform> onSeePlayer;

    public float viewAngle = 45f;
    public float seeDistance = 10f;

    private void Update()
    {
        SeePlayer();
    }

    private bool SeePlayer()
    {
        Vector3 toTarget = target.position - head.position;
        float distance = toTarget.magnitude;

        if (distance > seeDistance)
        {
            onSeePlayer?.Invoke(0, target);
            return false;
        }

        
        //Debug.Log("1 " + Vector3.Dot(transform.forward, toTarget));
        //Debug.Log("2 " + Mathf.Cos(viewAngle * Mathf.Deg2Rad));

        if (Physics.Raycast(head.position, target.position + Vector3.up * 0.1f, layerObstacle))
        {
            onSeePlayer?.Invoke(0, target);
            return false;
        }


        toTarget.Normalize();
        if (Vector3.Dot(transform.forward, toTarget) < Mathf.Cos(viewAngle * Mathf.Deg2Rad))
        {
            onSeePlayer?.Invoke(0, target);
            return false;
        }

        Debug.Log("See");
        Debug.DrawLine(head.position, target.position + Vector3.up * 0.1f);
        onSeePlayer?.Invoke(10,target);
        return true;
    }
}
