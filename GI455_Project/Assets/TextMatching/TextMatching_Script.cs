using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMatching_Script : MonoBehaviour
{

    public string inputWord;
    public GameObject inputField, resultDisplay, listDisplay;
    public string[] wordList;

    private void Start()
    {
        for(int i = 0; i < wordList.Length; i++)
        {
            listDisplay.GetComponent<Text>().text += (wordList[i] + "\n");
        }
    }

    public void SearchInputWord()
    {
        inputWord = inputField.GetComponent<Text>().text;
        
        for (int i = 0; i < wordList.Length; i++)
        {
            if (inputWord == wordList[i])
            {
                resultDisplay.GetComponent<Text>().text = "[ " + inputWord + " ]" + " is found.";
                break;
            }
            else
            {
                resultDisplay.GetComponent<Text>().text = "[ " + inputWord + " ]" + " is not found.";
            }
        }
    }
}
