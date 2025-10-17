using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D), typeof(Rigidbody2D))]
public class LinePhysics : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider;
    private Rigidbody2D rb;
    private float gravityForceMagnitude = 9.8f;
    private Vector2 customGravity = new Vector2(0f, -9.8f);
    public float lineWidth = 0.06f;

    public enum GravityDirection { Down, Up, Left, Right }

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        rb.AddForce(customGravity * rb.mass);
    }

    public void SetGravityDirection(GravityDirection dir)
    {
        switch (dir)
        {
            case GravityDirection.Down: customGravity = new Vector2(0f, -gravityForceMagnitude); break;
            case GravityDirection.Up: customGravity = new Vector2(0f, gravityForceMagnitude); break;
            case GravityDirection.Left: customGravity = new Vector2(-gravityForceMagnitude, 0f); break;
            case GravityDirection.Right: customGravity = new Vector2(gravityForceMagnitude, 0f); break;
        }
    }

    public void UpdatePolygonCollider(List<Vector2> linePoints)
    {
        if (linePoints.Count < 2) return;

        List<Vector2> vertices = new List<Vector2>();

        // Generate “ribbon” vertices for the polygon
        for (int i = 0; i < linePoints.Count; i++)
        {
            Vector2 dir;
            if (i == 0)
                dir = (linePoints[i + 1] - linePoints[i]).normalized;
            else if (i == linePoints.Count - 1)
                dir = (linePoints[i] - linePoints[i - 1]).normalized;
            else
                dir = ((linePoints[i + 1] - linePoints[i - 1]).normalized);

            Vector2 normal = new Vector2(-dir.y, dir.x) * (lineWidth / 2f);
            vertices.Add(linePoints[i] + normal);
        }
        for (int i = linePoints.Count - 1; i >= 0; i--)
        {
            Vector2 dir;
            if (i == 0)
                dir = (linePoints[i + 1] - linePoints[i]).normalized;
            else if (i == linePoints.Count - 1)
                dir = (linePoints[i] - linePoints[i - 1]).normalized;
            else
                dir = ((linePoints[i + 1] - linePoints[i - 1]).normalized);

            Vector2 normal = new Vector2(-dir.y, dir.x) * (lineWidth / 2f);
            vertices.Add(linePoints[i] - normal);
        }

        polygonCollider.pathCount = 1;
        polygonCollider.SetPath(0, vertices.ToArray());
    }

    void LateUpdate()
    {
        // Sync LineRenderer
        Vector2[] edgePoints = polygonCollider.GetPath(0);
        for (int i = 0; i < edgePoints.Length; i++)
        {
            Vector3 worldPos = rb.transform.TransformPoint(edgePoints[i]);
            if (i < lineRenderer.positionCount)
                lineRenderer.SetPosition(i, worldPos);
        }
    }
}
