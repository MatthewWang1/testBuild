using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaferRadialAnimation : MonoBehaviour
{
    public Image targetImage;
    
    public IEnumerator FadeOut()
    {
        // Debug.Log("etst");

        float elapsedTime = 0f;
        float fadeDuration = 0.5f;
        Color startColor = targetImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration); // Gradually decrease alpha
            targetImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        targetImage.color = new Color(startColor.r, startColor.g, startColor.b, 0); // Ensure alpha is 0 at the end
    }
    
}
