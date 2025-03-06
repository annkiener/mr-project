using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public Material newMaterial;
    private MeshRenderer meshRenderer;
    private Material originalMaterial;

    void Start()
    {
        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand") || collision.gameObject.CompareTag("LeftHand"))
        {
            // Change material
            if (meshRenderer != null && newMaterial != null)
            {
                meshRenderer.material = newMaterial;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Restore the original material when the hand stops touching
        if (collision.gameObject.CompareTag("Hand") || collision.gameObject.CompareTag("LeftHand"))
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = originalMaterial;
            }
        }
    }
}
