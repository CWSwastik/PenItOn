using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems; 

public class DrawingManager : MonoBehaviour
{
    public GameObject linePrefab;
    public float lineWidth = 0.06f;
    private GameObject currentLine;
    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider;
    private List<Vector2> points = new List<Vector2>();

    private LinePhysics.GravityDirection currentGravityType = LinePhysics.GravityDirection.Down;


    void Update()
    {
        // Only allow drawing while game is paused (before pressing Space)
        if (Time.timeScale == 0f)
        {
            if (EventSystem.current.IsPointerOverGameObject(-1))
                return;
                
            if (Input.GetKeyDown(KeyCode.S)) currentGravityType = LinePhysics.GravityDirection.Down;
            if (Input.GetKeyDown(KeyCode.W)) currentGravityType = LinePhysics.GravityDirection.Up;
            if (Input.GetKeyDown(KeyCode.A)) currentGravityType = LinePhysics.GravityDirection.Left;
            if (Input.GetKeyDown(KeyCode.D)) currentGravityType = LinePhysics.GravityDirection.Right;


            if (Input.GetMouseButtonDown(0))
                StartLine();
            else if (Input.GetMouseButton(0))
                ContinueLine();
            else if (Input.GetMouseButtonUp(0))
                FinishLine();
        }
    }

    void StartLine()
    {
        currentLine = Instantiate(linePrefab);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        polygonCollider = currentLine.GetComponent<PolygonCollider2D>();

        points.Clear();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Color c = Color.white; // default
        switch (currentGravityType)
        {
            case LinePhysics.GravityDirection.Down:
                c = Color.red;      // Down = Red
                break;
            case LinePhysics.GravityDirection.Up:
                c = Color.green;    // Up = Green
                break;
            case LinePhysics.GravityDirection.Left:
                c = Color.blue;     // Left = Blue
                break;
            case LinePhysics.GravityDirection.Right:
                c = Color.yellow;   // Right = Yellow
                break;
        }

        lineRenderer.startColor = c;
        lineRenderer.endColor = c;

        Vector2 startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        points.Add(startPoint);
        points.Add(startPoint);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint);
        LinePhysics lp = currentLine.GetComponent<LinePhysics>();
        lp.UpdatePolygonCollider(points);    
    }

    void ContinueLine()
    {
        Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(newPoint, points[points.Count - 1]) > 0.1f)
        {
            points.Add(newPoint);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count - 1, newPoint);
            LinePhysics lp = currentLine.GetComponent<LinePhysics>();
            lp.UpdatePolygonCollider(points);
        }
    }

    void FinishLine()
    {
        LinePhysics lp = currentLine.GetComponent<LinePhysics>();
        lp.SetGravityDirection(currentGravityType);

        lp.UpdatePolygonCollider(points);
        Debug.Log("set gravity as" + currentGravityType);
    
    }
}
