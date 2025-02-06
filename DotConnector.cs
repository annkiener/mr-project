using System.Collections.Generic;
using UnityEngine;

public class DotConnector : MonoBehaviour
{
    public static DotConnector Instance; // Singleton pattern to access from DotInteraction
    private List<Transform> connectedDots = new List<Transform>();
    private LineRenderer lineRenderer;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.positionCount = 0;
    }

    public void RegisterDot(Transform dotTransform)
    {
        if (!connectedDots.Contains(dotTransform))
        {
            connectedDots.Add(dotTransform);
            UpdateLine();
        }
    }

    private void UpdateLine()
    {
        lineRenderer.positionCount = connectedDots.Count;
        for (int i = 0; i < connectedDots.Count; i++)
        {
            lineRenderer.SetPosition(i, connectedDots[i].position);
        }
    }

    public void ResetConnections()
    {
        connectedDots.Clear();
        lineRenderer.positionCount = 0;
    }
}
