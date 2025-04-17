using UnityEngine;
using System.Collections;

public class ObjectCollision : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material originalMaterial;

    // Global shared audio stuff
    private static AudioSource sharedAudioSource;
    private static AudioClip targetClip;
    private static Coroutine repeatCoroutine;
    private static bool hasStopped = false;

    private static MonoBehaviour coroutineHost; // run the coroutine from one instance

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;
        }

        // Set up shared audio only once
        if (!hasStopped && sharedAudioSource == null)
        {
            GameObject audioObject = GameObject.FindGameObjectWithTag("Audio10");
            if (audioObject != null)
            {
                sharedAudioSource = audioObject.GetComponent<AudioSource>();
                if (sharedAudioSource != null)
                {
                    targetClip = sharedAudioSource.clip;

                    // Start the coroutine from the first instance
                    coroutineHost = this;
                    repeatCoroutine = coroutineHost.StartCoroutine(RepeatAudio());
                }
                else
                {
                    Debug.LogWarning("Audio10 object does not have an AudioSource!");
                }
            }
            else
            {
                Debug.LogWarning("No object with tag 'Audio10' found.");
            }
        }
    }

    static IEnumerator RepeatAudio()
    {
        yield return new WaitForSeconds(15f); // Initial delay before starting the loop because of the orchestrator
        while (!hasStopped)
        {
            sharedAudioSource.Play();

            float timer = 0f;
            while (timer < targetClip.length && !hasStopped)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            float pause = 0f;
            while (pause < 3f && !hasStopped)
            {
                pause += Time.deltaTime;
                yield return null;
            }
        }

        Debug.Log("Audio loop stopped.");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("HandIndex1"))
        {
            Debug.Log("Collision detected by " + gameObject.name);

            // Change color 
            if (meshRenderer != null)
            {
                Color randomColor = new Color(Random.value, Random.value, Random.value);
                meshRenderer.material.color = randomColor;
            }

            // Stop the shared audio 
            if (!hasStopped)
            {
                hasStopped = true;

                if (repeatCoroutine != null && coroutineHost != null)
                {
                    coroutineHost.StopCoroutine(repeatCoroutine);
                }

                if (sharedAudioSource != null && sharedAudioSource.isPlaying)
                {
                    sharedAudioSource.Stop();
                }

                Debug.Log("Audio loop stopped due to collision with: " + gameObject.name);
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = originalMaterial;
            }
        }
    }
}
