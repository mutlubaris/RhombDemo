using UnityEngine;

public class LineUpdate : MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Start() 
    {
        //Determine the position count of the line
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = transform.childCount;
    }
    
    private void Update()
    {
        //Keep updating the line positions as the object moves
        
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, transform.GetChild(i).transform.position);
        }
    }

    private void OnDrawGizmos() 
    {
        //Draw gizmo lines to preview the route during editing
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = transform.childCount;
        
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
}
