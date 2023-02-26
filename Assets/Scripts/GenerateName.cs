using System.Collections;
using System.Collections.Generic;
using SFB;
using UnityEngine;
using System.IO;
using TMPro;

public class GenerateName : MonoBehaviour
{
    public TMP_InputField courseNameField, firstFileNameField, secondFileNameField;
    public TextAsset firstDefaultFile, secondDefaultFile;
    string firstFilePath = "", secondFilePath = "";
    List<string> firstParts, secondParts;

    ExtensionFilter[] extensions = new[] {
                new ExtensionFilter("Text Files", "txt"),
                new ExtensionFilter("All Files", "*" ),
            };


    private void Start()
    {
        firstParts = new List<string>(firstDefaultFile.text.Replace("\r", "").Split('\n'));
        secondParts = new List<string>(secondDefaultFile.text.Replace("\r", "").Split('\n'));
    }

    public void Generate()
    {
        int N = firstParts.Count, M = secondParts.Count;
        string first = firstParts[Random.Range(0, N)], second = secondParts[Random.Range(0, M)];
        if (first.Contains("--R") && !second.Contains("--NR"))
        {
            first = first.Replace("--R", "").Trim();
            var array = second.ToCharArray();
            array[0] = (char)('A' - 'a' + array[0]);
            second = new string(array);
            courseNameField.text = second + " " + first;
        }
        else
        {
            first = first.Replace("--R", "").Trim();
            second = second.Replace("--NR", "").Trim();
            courseNameField.text = first + " " + second;
        }
    }

    public void ReadFirstFile()
    {
        firstFilePath = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true)[0];
        firstParts = new List<string>(File.ReadAllLines(firstFilePath));
        firstFileNameField.text = firstFilePath.Split('\\')[firstFilePath.Split('\\').Length - 1];
    }

    public void ReadSecondFile()
    {
        secondFilePath = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true)[0];
        secondParts = new List<string>(File.ReadAllLines(secondFilePath));
        secondFileNameField.text = secondFilePath.Split('\\')[secondFilePath.Split('\\').Length - 1];
    }

    public void Quit()
    {
        Application.Quit();
    }
}
