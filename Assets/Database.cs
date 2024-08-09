using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private string[] ArrayOfWord = { "apple", "orange" };
    private string[] ArrayOfSentence = { "She read a book in the park.", "The car drove down the street." };
    private string[] ArrayOfPassage = { "The young boy ran through the park, chasing after his dog. He laughed happily enjoying the fresh outdoor air." };

    private List<string> myList = new List<string>();

    private void Start()
    {
        foreach (string item in ArrayOfWord)
        {
            myList.Add(item);
        }

        foreach (string item in ArrayOfSentence)
        {
            myList.Add(item);
        }

        foreach (string item in ArrayOfPassage)
        {
            myList.Add(item);
        }
    }

    public string getData(int index)
    {
        return myList[index];
    }

}

