using System;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;

    [System.Serializable]
    public class WordDisplay
    {
        public string word;
        public string sentence;
        public string passage;
    }

    public class WordDisplayList
    {
        public WordDisplay[] display;
    }

    public WordDisplayList myWordDisplayList = new WordDisplayList();

    private System.Random random = new System.Random();

    void Start()
    {
        ReadCSV();
        Debug.Log(GetRandomWord());
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 3 - 1;
        myWordDisplayList.display = new WordDisplay[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myWordDisplayList.display[i] = new WordDisplay();
            myWordDisplayList.display[i].word = data[3 * (i + 1)];
            myWordDisplayList.display[i].sentence = data[3 * (i + 1) + 1];
            myWordDisplayList.display[i].passage = data[3 * (i + 1) + 2];
        }
    }

    public WordDisplay GetRandomRowAndColumn()
    {
        int randomIndex = random.Next(0, myWordDisplayList.display.Length);
        return myWordDisplayList.display[randomIndex];
    }

    public string GetRandomWord()
    {
        WordDisplay randomRow = GetRandomRowAndColumn();
        return randomRow.word;
    }

    public string GetRandomSentence()
    {
        WordDisplay randomRow = GetRandomRowAndColumn();
        return randomRow.sentence;
    }

    public string GetRandomPassage()
    {
        WordDisplay randomRow = GetRandomRowAndColumn();
        return randomRow.passage;
    }
}