using UnityEngine;
using System.Collections.Generic;

public class DotManager : MonoBehaviour
{
    public GameObject dotPrefab; // Assign the DotPrefab in the Inspector
    public int numberOfDots = 100; // Total number of dots to spawn
    public float spawnWidth = 5.0f; // Horizontal range
    public float spawnHeight = 1.2f; // Vertical range (since player eye level is ~1.5m)
    public float spawnDepth = 0.1f; // Depth range in front of player
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    

    public float minDistance = 0.2f; // Minimum distance between dots

    public List<GameObject> spawnedDots = new List<GameObject>(); // List to store spawned dots


    void Start()
    {

        InstantiateDots();
    }

    public void InstantiateDots()
    {
        int attempts;
        for (int i = 0; i < numberOfDots; i++)
        {
            Vector3 randomOffset = Vector3.zero;
            bool positionFound = false;
            attempts = 0;

            while(!positionFound && attempts < 100)
            {
                // Generate a random position within a specified range
                randomOffset = new Vector3(
                Random.Range(-spawnWidth, spawnWidth), // Left/Right range
                Random.Range(1.4f, spawnHeight), // Height range (1m to 1.5m)
                Random.Range(0.5f, spawnDepth) // Depth in front of player
            );

            positionFound = true;
                foreach (GameObject dot in spawnedDots)
                {
                    if (Vector3.Distance(dot.transform.position, randomOffset) < minDistance)
                    {
                        positionFound = false;
                        break;
                    }

                }
            attempts++;

            }

            if(positionFound)
            {
                // Instantiate the dot prefab at the random position
                GameObject dot = Instantiate(dotPrefab, randomOffset, Quaternion.identity);

                // Store the dot reference in the list
                spawnedDots.Add(dot);

                // Randomly assign red or blue color
                Renderer dotRenderer = dot.GetComponent<Renderer>();
                dotRenderer.material.color = Random.value > 0.5f ? redColor : blueColor;

            }      

            else
            {
                Debug.LogWarning("Failed to find a valid position for the dot.");     

            }
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
