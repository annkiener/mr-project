using System.Collections;
using UnityEngine;

public class Orchestrator : MonoBehaviour
{
    public AudioSource whispers; // Reference to the background whispers
    public SoundManager soundManager; // Reference to the SoundManager script
    public GameObject spherePrefab; // Assign the pulsating sphere prefab in Inspector
    public Transform spawnPoint; // Location where spheres will appear
    public float spawnRadius = 10f;
    public int sphereCount = 7; // How many spheres to spawn
    public float spawnInterval = 5f; // Time between spawns
    public DotManager dotManager; // Reference to the DotManager script

    void Start()
    {
        StartCoroutine(OrchestrationSequence());
        
    }

    IEnumerator OrchestrationSequence()
    {
        Debug.Log("Simulation started");

        // will be 60s but temporarily it is set to 10s for testing
        yield return new WaitForSeconds(10f);
        PlayWhispers();
        // visual field is getting darker

        yield return new WaitForSeconds(5f);
        soundManager.PlaySound("1");

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("2");

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("3");

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("4");

        yield return new WaitForSeconds(20f);
        for (int i = 0; i < sphereCount; i++)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnSphere();      
            }
        soundManager.PlaySound("5");

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("6");

        yield return new WaitForSeconds(5f);
        soundManager.PlaySound("7");

        yield return new WaitForSeconds(5f);
        // dark spots slowly fade away   

        yield return new WaitForSeconds(10f);
        // DotsAppear();
        soundManager.PlaySound("8");

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("9");    

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("10");

        yield return new WaitForSeconds(20f);
        // dots disappear
        soundManager.PlaySound("11");


        yield return new WaitForSeconds(120f);
        Debug.Log("Simulation Ended");
    }

    void PlayWhispers()
    {
        if (whispers != null)
        {
            whispers.loop = true;
            whispers.Play();
            Debug.Log("Whispers started.");
        }
        else
        {
            Debug.LogError(" AudioSource is not assigned!");
        }
    }

    void SpawnSphere()
    {
        if (spherePrefab == null)
        {
            Debug.LogError("Sphere Prefab or Spawn Point is not assigned!");
            return;
        }

         // Generate a random position within a circular area
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        randomOffset.y = 0; // Keep spheres on the same Y level
        Vector3 spawnPosition = spawnPoint.position + randomOffset;

        // Instantiate a new sphere at the random position
        GameObject newSphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);

        Debug.Log($"Spawned a sphere at {spawnPosition}");

    }

    void DotsAppear()
    {
        Debug.Log("Spwaning dots");
        
        if(dotManager != null)
        {
            dotManager.InstantiateDots(); // Trigger the dots
        }
        else
        {
            Debug.LogError("DotManager is not assigned in the Orchestrator!");
        }
    }
}
