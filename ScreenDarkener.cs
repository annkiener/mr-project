using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenDarkener : MonoBehaviour
{
    public Image darkPanel; // Assign this in the Inspector
    public float fadeDuration = 2f; // Time to darken in seconds

    void Start()
    {
        if (darkPanel != null)
        {
            StartCoroutine(FadeToDark());
        }
    }

    IEnumerator FadeToDark()
    {
        yield return new WaitForSeconds(10f); // Wait before starting

        float elapsedTime = 0f;
        Color panelColor = darkPanel.color;
        panelColor.a = 0; // Ensure it starts fully transparent

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelColor.a = Mathf.Lerp(0, 0.7f, elapsedTime / fadeDuration); // Adjust opacity (0 = invisible, 1 = fully black)
            darkPanel.color = panelColor;
            yield return null; // Wait for the next frame
        }

        panelColor.a = 0.7f; // Ensure it reaches final darkness level
        darkPanel.color = panelColor;
    }
}