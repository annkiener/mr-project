using UnityEngine;
using System.Collections.Generic;

public class DotManager : MonoBehaviour
{
    public GameObject dotPrefab; // Assign the DotPrefab in the Inspector
    public int numberOfDots = 100; // Total number of dots to spawn
    public float areaSize = 1.0f; // Size of the spawning area
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    

    public List<GameObject> spawnedDots = new List<GameObject>(); // List to store spawned dots


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

            // Store the dot reference in the list
            spawnedDots.Add(dot);

            // Randomly assign red or blue color
            Renderer dotRenderer = dot.GetComponent<Renderer>();
            dotRenderer.material.color = Random.value > 0.5f ? redColor : blueColor;

        }
    }

        public void RemoveDots()
    {
        // Loop through the list and destroy all spawned dots
        foreach (GameObject dot in spawnedDots)
        {
            if (dot != null)
            {
                Destroy(dot);
            }
        }

        // Clear the list after removing dots
        spawnedDots.Clear();
    }

}
