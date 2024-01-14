using UnityEngine;
using UnityEngine.UI;

public class FaderTest : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 2.0f;

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        Color fixedColor = image.color;
        fixedColor.a = 1;
        image.color = fixedColor;
        image.CrossFadeAlpha(0f, 0f, true);
        image.CrossFadeAlpha(1f, fadeDuration, false);
        //Color color = image.color;
        //color.a = 1.0f; // Set alpha to 1 (fully opaque)
        //image.color = color;
        //image.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        //FadeIn();
    }

    public void FadeIn()
    {
        // Fade to opaque
        Debug.Log("FadeIn");
        image.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    }

    public void FadeOut()
    {
        // Fade to transparent
        //image?.CrossFadeAlpha(1.0f, fadeDuration, false);
    }
}