using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui : MonoBehaviour
{
    
    public RawImage rawImage;

    public void FadeTo(float targetAlpha, float duration)
    {
        StartCoroutine(FadeAlphaCoroutine(targetAlpha, duration));
    }

    private IEnumerator FadeAlphaCoroutine(float targetAlpha, float duration)
    {
        Color currentColor = rawImage.color;
        float startAlpha = currentColor.a;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            rawImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        rawImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

}
