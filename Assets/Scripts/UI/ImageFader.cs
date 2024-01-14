using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 2.0f;

    public void FadeIn()
    {
        // Fade to opaque
        Color fixedColor = image.color;
        fixedColor.a = 1;
        image.color = fixedColor;
        image.CrossFadeAlpha(0f, 0f, true);
        image?.CrossFadeAlpha(1.0f, fadeDuration, false);
    }

    public void FadeOut()
    {
        // Fade to transparent
        image?.CrossFadeAlpha(0.0f, fadeDuration, false);
    }
}