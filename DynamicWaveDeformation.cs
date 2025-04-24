using UnityEngine;

public class DynamicWaveDeformation : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;

    public float waveAmplitude = 0.5f; // height
    public float waveFrequency = 1f;  // speed

    void Start()
    {
        // Get the mesh and its original vertices
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 originalVertex = originalVertices[i];

            // sine function for wave effect
            float wave = Mathf.Sin(Time.time * waveFrequency + originalVertex.x * waveFrequency) *
                         Mathf.Cos(Time.time * waveFrequency + originalVertex.z * waveFrequency);

            // modify the vertex position
            displacedVertices[i] = originalVertex + originalVertex.normalized * wave * waveAmplitude;
        }

        // update  mesh with the  vertex positions
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals(); 
    }
}
