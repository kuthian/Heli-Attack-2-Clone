using UnityEngine;
using UnityEngine.UI;

public class ImageFaderGroup : MonoBehaviour
{
    public ImageFader[] imageFaders;

    public void SetAlpha(float alpha)
    {
        foreach (var imageFader in imageFaders)
        {
            var image = imageFader.gameObject.GetComponentInChildren<Image>();
            if (image != null) {
                Debug.Log("image");
            }
            var cr = image.GetComponent<CanvasRenderer>();
            if (cr != null) {
                Debug.Log("CanvasRenderer");
            }
            cr.SetAlpha(alpha);
        }
    }

    public void FadeIn()
    {
        foreach(var imageFader in imageFaders)
        {
            imageFader?.FadeIn();
        }
    }

    public void FadeOut()
    {
        foreach(var imageFader in imageFaders)
        {
            imageFader?.FadeOut();
        }
    }
}