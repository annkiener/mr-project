using UnityEngine;
using System.Collections.Generic;

public class DotManager : MonoBehaviour
{
    public GameObject dotPrefab;
    public int numberOfDots = 100; // change in Unity
    public float spawnWidth = 5.0f; // horizontal
    public float spawnHeight = 1.2f; // vertical (above eye level)
    public float spawnDepth = 0.1f; 
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    

    public float minDistance = 0.2f; // between dots

    public List<GameObject> spawnedDots = new List<GameObject>(); 



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
                randomOffset = new Vector3(
                Random.Range(-spawnWidth, spawnWidth), 
                Random.Range(1.4f, spawnHeight), //height
                Random.Range(0.5f, spawnDepth) // depth (in front of user)
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
                // Instantiate
                GameObject dot = Instantiate(dotPrefab, randomOffset, Quaternion.identity);
                spawnedDots.Add(dot);

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
        foreach (GameObject dot in spawnedDots)
        {
            if (dot != null)
            {
                Destroy(dot);
            }
        }
        spawnedDots.Clear();
    }

}
