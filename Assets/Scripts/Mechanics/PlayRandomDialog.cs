using TMPro;
using UnityEngine;

public class PlayRandomDialog : MonoBehaviour
{
    public TypingEffect typingEffect;
    public float typingSpeed = 0.05f;
    public string[] possibleDialogs;

    public void Play()
    {
        typingEffect.Type(Utils.RandomInRange(possibleDialogs), typingSpeed);
    }
}