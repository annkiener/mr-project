using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenDarkener : MonoBehaviour
{
    public Image darkPanel; 
    public float fadeDuration = 2f; 

    void Start()
    {
        if (darkPanel != null)
        {
            // prevent this bright flash in the beginning
            Color startColor = darkPanel.color;
            startColor.a = 0f;
            darkPanel.color = startColor;
            StartCoroutine(FadeToDark());
        }
    }

    IEnumerator FadeToDark()
    {
        yield return new WaitForSeconds(10f); 

        float elapsedTime = 0f;
        Color panelColor = darkPanel.color;
        panelColor.a = 0.3f; // transparent

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // 0.7 so it is not fully black (1 would be fully black)
            panelColor.a = Mathf.Lerp(0, 0.7f, elapsedTime / fadeDuration); 
            darkPanel.color = panelColor;
            yield return null; // wait for next frame
        }

        panelColor.a = 0.7f; 
        darkPanel.color = panelColor;
    }
}