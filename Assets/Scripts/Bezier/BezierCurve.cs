using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float distance = 1f;
    [SerializeField] int vertexCount = 12;

    private bool endedTurn;
    private bool startedTurn;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 point3;

    private void Start() 
    {
        point1 = Vector3.MoveTowards(transform.GetChild(1).position, transform.GetChild(0).position, distance);
        point2 = transform.GetChild(1).position;
        point3 = Vector3.MoveTowards(transform.GetChild(1).position, transform.GetChild(2).position, distance);

        List<Vector3> pointList = new List<Vector3>();

        for (float ratio = 0.5f / vertexCount; ratio < 1; ratio += 1f / vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(point1, point2, ratio);
            var tangentLineVertex2 = Vector3.Lerp(point2, point3, ratio);
            var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            pointList.Add(bezierPoint);
        }

        lineRenderer.positionCount = pointList.Count + 4;

        lineRenderer.SetPosition(0, transform.GetChild(0).position);
        lineRenderer.SetPosition(1, point1);
        lineRenderer.SetPosition(pointList.Count + 2, point3);
        lineRenderer.SetPosition(pointList.Count + 3, transform.GetChild(2).position);

        for (int i = 0; i < pointList.Count; i++)
        {
            lineRenderer.SetPosition(i + 2, pointList[i]);
        }
    }

    private void Update() 
    {
        if (!startedTurn)
        {
            lineRenderer.SetPosition(0, transform.GetChild(0).position);
        }
        
        if (transform.GetChild(0).position == lineRenderer.GetPosition(1))
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, point3);
            lineRenderer.SetPosition(1, transform.GetChild(2).position);
            startedTurn = true;
        }

        if (startedTurn && transform.GetChild(0).position == lineRenderer.GetPosition(0))
        {
            endedTurn = true;
        }

        if (endedTurn)
        {
            lineRenderer.SetPosition(0, transform.GetChild(0).position);
        }
    }
    
    private void OnDrawGizmos() 
    {
        Vector3 point1 = Vector3.MoveTowards(transform.GetChild(1).position, transform.GetChild(0).position, distance);
        Vector3 point2 = transform.GetChild(1).position;
        Vector3 point3 = Vector3.MoveTowards(transform.GetChild(1).position, transform.GetChild(2).position, distance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.GetChild(0).position, point1);
        Gizmos.DrawLine(point3, transform.GetChild(2).position);
        for (float ratio = 0.5f / vertexCount; ratio < 1; ratio += 1f / vertexCount)
        {
            Gizmos.DrawLine(Vector3.Lerp(point1, point2, ratio), Vector3.Lerp(point2, point3, ratio));
        }
    }
}
