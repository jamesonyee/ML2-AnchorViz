using System.Collections;
using UnityEngine;
using TMPro;

public class ErrorLogger : MonoBehaviour
{
    public TextMeshProUGUI errorText; // Reference to the Text (TMP) component
    public float displayDuration = 5f; // Duration to display each error message

    private void Start()
    {
        // Initialize the text if necessary
        errorText.text = "Issues:\n";
    }

    // Method to display error messages
    public void DisplayError(string message)
    {
        StartCoroutine(DisplayErrorCoroutine(message));
    }

    private IEnumerator DisplayErrorCoroutine(string message)
    {
        // Append the new error message to the existing text
        errorText.text += message + "\n";

        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Remove the message after the duration
        errorText.text = errorText.text.Replace(message + "\n", "");
    }
}