using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConoVisivo : MonoBehaviour
{
    [SerializeField] private float viewAngle = 45;
    [SerializeField] private int pointLineRed = 50;
    [SerializeField] private int sightDistance = 40;
    [SerializeField] private LayerMask layerObstacol;
    [SerializeField] private LineRenderer lineRendView;

    private void Start()
    {
        lineRendView.loop = true;
    }

    private void Update()
    {
        lineRendView.positionCount = pointLineRed;

        Vector3 lineOri = transform.position;
        Vector3 rayCastOrigin = transform.position + new Vector3(0, 0.1f, 0);
        Vector3 forw = transform.forward;

        lineRendView.SetPosition(0, lineOri);
        float deltaAngle = (2 * viewAngle / pointLineRed);

        for (int i = 0; i < pointLineRed; i++)
        {
            float currentAngle = -viewAngle + deltaAngle * i;
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * forw;
            Vector3 point = lineOri + direction * sightDistance;

            if(Physics.Raycast(rayCastOrigin,direction,out RaycastHit hit, sightDistance, layerObstacol))
            {
                point = hit.point;
            }

            lineRendView.SetPosition(i,point);
        }

        
    }

}
