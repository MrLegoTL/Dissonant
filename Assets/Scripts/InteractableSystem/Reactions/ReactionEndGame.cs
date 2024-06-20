using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionEndGame : Reaction
{
    public Image whiteImage;
    public Image newImage; 
    public float fadeDuration = 2.0f; 
    public float displayDuration = 2.0f; 
    protected override void React()
    {
        StartFadeToWhite();
    }

    // Método para iniciar el fundido a blanco
    public void StartFadeToWhite()
    {
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // Fundido a blanco
        yield return StartCoroutine(FadeIn(whiteImage));
        if(newImage!= null)
        {
            // Mostrar la nueva imagen
            yield return StartCoroutine(FadeIn(newImage));
        }
        

        // Mantener la nueva imagen visible por un tiempo
        yield return new WaitForSeconds(displayDuration);

        
    }

    private IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }


}
