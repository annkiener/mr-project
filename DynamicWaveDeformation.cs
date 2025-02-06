using UnityEngine;

public class DynamicWaveDeformation : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;

    public float waveAmplitude = 0.5f; // Height of the wave
    public float waveFrequency = 1f;  // Speed of the wave

    void Start()
    {
        // Get the mesh and its original vertices
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        // Create a dynamic wave effect
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Get the original vertex position
            Vector3 originalVertex = originalVertices[i];

            // Apply a wave effect using sine function
            float wave = Mathf.Sin(Time.time * waveFrequency + originalVertex.x * waveFrequency) *
                         Mathf.Cos(Time.time * waveFrequency + originalVertex.z * waveFrequency);

            // Modify the vertex position
            displacedVertices[i] = originalVertex + originalVertex.normalized * wave * waveAmplitude;
        }

        // Update the mesh with the new vertex positions
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals(); // Recalculate normals for proper lighting
    }
}
