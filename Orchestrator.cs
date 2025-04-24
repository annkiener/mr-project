using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour
{
    public AudioSource whispers; 
    public SoundManager soundManager; 
    public GameObject spherePrefab; 
    public Transform spawnPoint; 
    public float spawnRadius = 10f;
    public int sphereCount = 10; 
    public float spawnInterval = 2f; 

    private List<GameObject> spawnedSpheres = new List<GameObject>();

    public float disappearInterval = 2f; 
    public DotManager dotManager; 

    void Start()
    {
        StartCoroutine(OrchestrationSequence());
        
    }

    IEnumerator OrchestrationSequence()
    {
        Debug.Log("Simulation started");
        
        // will be 60s but temporarily it is set to 10s for testing
        yield return new WaitForSeconds(60f);
        PlayWhispers();
        
        // visual field is getting darker
        
        yield return new WaitForSeconds(5f);
        
        soundManager.PlaySound("1");

        yield return new WaitForSeconds(15f);
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

        yield return new WaitForSeconds(5f);
        soundManager.PlaySound("5");

        yield return new WaitForSeconds(5f);
        soundManager.PlaySound("5");

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("6");

        yield return new WaitForSeconds(5f);
        soundManager.PlaySound("7");

        yield return new WaitForSeconds(5f);
        while (spawnedSpheres.Count > 0)
        {
            yield return new WaitForSeconds(disappearInterval);

            //  first spawned sphere
            GameObject sphereToRemove = spawnedSpheres[0];
            spawnedSpheres.RemoveAt(0);

            if (sphereToRemove != null)
            {
                Destroy(sphereToRemove);
            }
        } 

        yield return new WaitForSeconds(20f);
        DotsAppear();
        soundManager.PlaySound("8");

        yield return new WaitForSeconds(10f);
        soundManager.PlaySound("9");    

        //yield return new WaitForSeconds(10f);
        //soundManager.PlaySound("10");

        yield return new WaitForSeconds(20f);
        soundManager.PlaySound("11");

        yield return new WaitForSeconds(10f);
        DotsDisappear();
        soundManager.PlaySound("12");


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

        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        randomOffset.y = 0; // same Y level
        Vector3 spawnPosition = spawnPoint.position + randomOffset;
        GameObject newSphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);

        // Store reference to the sphere
        spawnedSpheres.Add(newSphere);

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

    void DotsDisappear()
    {
        dotManager.RemoveDots();
    }

}
