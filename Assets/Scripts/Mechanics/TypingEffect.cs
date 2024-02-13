using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public void Type(string text, float typingSpeed)
    {
        StartCoroutine(_TypeText(text, typingSpeed));
    }

    private IEnumerator _TypeText(string textToType, float typingSpeed)
    {
        dialogText.text = ""; // Clear the text field
        foreach (char letter in textToType.ToCharArray())
        {
            dialogText.text += letter; // Add each letter to the text field
            yield return new WaitForSeconds(typingSpeed); // Wait a bit before adding the next letter
        }
    }
}
