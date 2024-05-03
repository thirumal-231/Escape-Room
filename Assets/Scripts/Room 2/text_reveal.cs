using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class text_reveal : MonoBehaviour
{
    public float revealDelay = 0.2f; // Delay between revealing each character
    private TMP_Text textComponent;
    private string fullText;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        fullText = textComponent.text;
        textComponent.text = ""; // Clear the text initially
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i]; // Append one character at a time
            yield return new WaitForSeconds(revealDelay); // Wait for the delay before revealing the next character
        }
    }
}
