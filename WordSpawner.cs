using UnityEngine;
using System.Collections.Generic;
using TMPro; // Import TextMeshPro

public class WordSpawner : MonoBehaviour
{
    public TextMeshPro textPrefab; // Assign a TextMeshPro prefab in the Inspector
    public int numberOfWords = 20; // Total number of words to spawn
    public float areaWidth = 5.0f; // Full horizontal range
    public float areaHeight = 2.5f; // Full vertical range (from near the floor to above)
    public float areaDepth = 5.0f; // Full depth range
    public float minDistance = 0.3f; // Minimum distance between words to avoid overlapping

    public string[] words = { "Useless", "Dumb", "Attention!", "Careful", "Watch" }; // Custom words

    private List<GameObject> spawnedWords = new List<GameObject>();

    void Start()
    {
        InstantiateWords();
    }

    public void InstantiateWords()
    {
        int attempts;
        for (int i = 0; i < numberOfWords; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            bool positionFound = false;
            attempts = 0;

            // Try to find a valid non-overlapping position
            while (!positionFound && attempts < 100)
            {
                randomPosition = new Vector3(
                    Random.Range(-areaWidth, areaWidth), // Full X range
                    Random.Range(0.5f, areaHeight), // Height range (above floor)
                    Random.Range(-areaDepth, areaDepth) // Full Z range
                );

                positionFound = true;
                foreach (GameObject word in spawnedWords)
                {
                    if (Vector3.Distance(randomPosition, word.transform.position) < minDistance)
                    {
                        positionFound = false;
                        break;
                    }
                }
                attempts++;
            }

            if (positionFound)
            {
                // Instantiate TextMeshPro object at random position
                TextMeshPro newText = Instantiate(textPrefab, randomPosition, Quaternion.identity);
                newText.text = words[Random.Range(0, words.Length)]; // Assign a random word

                // Customize text appearance
                newText.fontSize = Random.Range(3f, 6f); // Random font size for variety
                newText.color = Color.black;
                newText.alignment = TextAlignmentOptions.Center;

                spawnedWords.Add(newText.gameObject);
            }
            else
            {
                Debug.LogWarning("Could not find a valid position for word " + i);
            }
        }
    }

    public void RemoveWords()
    {
        foreach (GameObject word in spawnedWords)
        {
            if (word != null)
            {
                Destroy(word);
            }
        }
        spawnedWords.Clear();
    }
}
