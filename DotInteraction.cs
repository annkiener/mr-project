using UnityEngine;

public class DotInteraction : MonoBehaviour
{
    private Renderer dotRenderer;
    private Color originalColor;
    private bool isTouched = false;

    void Start()
    {
        dotRenderer = GetComponent<Renderer>();
        originalColor = dotRenderer.material.color;
    }

    void OnMouseDown() // Works for PC (Mouse) & Mobile (Touch)
    {
        if (!isTouched) // Prevent multiple touches
        {
            isTouched = true;
            dotRenderer.material.color = Color.yellow; // Light up
            DotConnector.Instance.RegisterDot(this.transform);
        }
    }

    public void ResetDot()
    {
        isTouched = false;
        dotRenderer.material.color = originalColor; // Reset to original color
    }
}
