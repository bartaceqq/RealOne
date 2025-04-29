using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnableText : MonoBehaviour
{
    public Image image;
    public TMP_Text uiText;
    private string currentTextFile = "WitchText"; // Default file at start

    private string[] lines;
    public float letterDelay = 0.05f;

    public void LoadNewTextFile(string fileName)
    {
        currentTextFile = fileName;
        TextAsset textAsset = Resources.Load<TextAsset>(currentTextFile);
        if (textAsset != null)
        {
            lines = textAsset.text.Split('\n');
        }
        else
        {
            Debug.LogError($"{fileName}.txt not found in Resources!");
        }
    }

    // Method to parse text and apply color formatting based on *text* and /text/ markers
    private string ParseTextWithColorTags(string input)
    {
        string parsedText = "";
        bool isPurple = false;
        bool isOrange = false;

        for (int i = 0; i < input.Length; i++)
        {
            // Check for the start of *text* (purple)
            if (i + 1 < input.Length && input[i] == '*' && !isPurple)
            {
                parsedText += "<color=#800080>"; // Purple start tag
                isPurple = true;
                continue;
            }

            // Check for the end of *text* (purple)
            if (i + 1 < input.Length && input[i] == '*' && isPurple)
            {
                parsedText += "</color>"; // Purple end tag
                isPurple = false;
                continue;
            }

            // Check for the start of /text/ (orange)
            if (i + 1 < input.Length && input[i] == '/' && !isOrange)
            {
                parsedText += "<color=#FFA500>"; // Orange start tag
                isOrange = true;
                continue;
            }

            // Check for the end of /text/ (orange)
            if (i + 1 < input.Length && input[i] == '/' && isOrange)
            {
                parsedText += "</color>"; // Orange end tag
                isOrange = false;
                continue;
            }

            // Add regular characters to the parsedText
            parsedText += input[i];
        }

        return parsedText; // Return the parsed string with proper color tags
    }

    public IEnumerator ReadTextLetterByLetter()
    {
        uiText.enabled = true;
        image.enabled = true;

        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("No lines loaded!");
            yield break;
        }

        uiText.text = "";

        foreach (string line in lines)
        {
            string formattedLine = ParseTextWithColorTags(line);

            int i = 0;
            while (i < formattedLine.Length)
            {
                // If this is the start of a tag
                if (formattedLine[i] == '<')
                {
                    // Find the end of the tag
                    int endTagIndex = formattedLine.IndexOf('>', i);
                    if (endTagIndex != -1)
                    {
                        // Add the full tag instantly
                        uiText.text += formattedLine.Substring(i, endTagIndex - i + 1);
                        i = endTagIndex + 1;
                        continue;
                    }
                }

                // Add one visible letter
                uiText.text += formattedLine[i];
                i++;

                // Wait only after real letters
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(1f); // Wait after each line
            uiText.text = ""; // Clear after each line if you want
        }

        uiText.enabled = false;
        image.enabled = false;
    }


    void Start()
    {
        // Load default text at the beginning
        LoadNewTextFile(currentTextFile);
    }
}
