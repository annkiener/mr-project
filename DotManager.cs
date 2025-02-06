using UnityEngine;

public class DotManager : MonoBehaviour
{
    public GameObject dotPrefab; // Assign the DotPrefab in the Inspector
    public int numberOfDots = 100; // Total number of dots to spawn
    public float areaSize = 5.0f; // Size of the spawning area
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;

    void Start()
    {
        InstantiateDots();
    }

    public void InstantiateDots()
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            // Generate a random position within a specified range
            Vector3 randomPosition = new Vector3(
                Random.Range(-areaSize, areaSize),
                Random.Range(-areaSize, areaSize),
                Random.Range(-areaSize, areaSize)
            );

            // Instantiate the dot prefab at the random position
            GameObject dot = Instantiate(dotPrefab, randomPosition, Quaternion.identity);
            dot.AddComponent<DotInteraction>(); // Add DotInteraction script to the dot

            // Randomly assign red or blue color
            Renderer dotRenderer = dot.GetComponent<Renderer>();
            dotRenderer.material.color = Random.value > 0.5f ? redColor : blueColor;

        }
    }

        public void ResetDots()
    {
        DotConnector.Instance.ResetConnections();
        foreach (DotInteraction dot in FindObjectsOfType<DotInteraction>())
        {
            dot.ResetDot();
        }
    }
}
