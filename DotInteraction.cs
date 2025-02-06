using UnityEngine;

public class DotInteraction : MonoBehaviour
{
    public Color touchColor = Color.green; // Change color on touch

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Detects the hand colliders
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = touchColor;
        }
    }
}
