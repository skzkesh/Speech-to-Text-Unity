using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text.RegularExpressions;

public class STT : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private TextMeshProUGUI wordUserSay;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI instruction;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CSVReader csvReader;

    public Image startButtonImage;
    public Image stopButtonImage;

    //to handle speech-to-text
    private AudioClip clip;
    private byte[] bytes;
    private bool recording;
    private string wordToProcess;

    //storing data from CSV
    private int currIndex = 0;
    private int randomIndex;
    private string[] answerArr = new string[300];

    //other features
    private int scoreNum = 0;
    private float timeRemaining = 60f;


    private void Start()
    {
        StartCoroutine(CountdownCoroutine()); //timer
        startButton.onClick.AddListener(StartRecording);
        stopButton.onClick.AddListener(StopRecording);
        skipButton.onClick.AddListener(SkipButtonClick);
        stopButton.interactable = false;

        //randomly generate an array of words
        for (int i = 0; i < 300; i++)
        {
            randomIndex = Random.Range(0, 3);
            switch (randomIndex)
            {
                case 0:
                    answerArr[i] = csvReader.GetRandomWord();
                    break;
                case 1:
                    answerArr[i] = csvReader.GetRandomSentence();
                    break;
                case 2:
                    answerArr[i] = csvReader.GetRandomPassage();
                    break;
            }
        }

        displayCorrectWordFromArray();
    }

    public void Update()
    {
        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }
    }

    private void StartRecording()
    {
        wordUserSay.color = Color.white;
        wordUserSay.text = "Recording...";
        startButton.interactable = false;
        stopButton.interactable = true;
        clip = Microphone.Start(null, false, 30, 44100);
        recording = true;
        startButtonImage.color = new Color(1f, 0.8f, 0f);
    }

    private void StopRecording()
    {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();
        stopButtonImage.color = new Color(1f, 0.8f, 0f);
    }

    private void SendRecording()
    {
        startButtonImage.color = Color.yellow;
        stopButtonImage.color = Color.yellow;
        wordUserSay.color = Color.yellow;
        wordUserSay.text = "Sending...";
        stopButton.interactable = false;
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response =>
        {
            wordUserSay.color = Color.black;
            wordUserSay.text = response;
            wordToProcess = response;
            wordToProcess = RemoveLeadingSpace(wordToProcess);
            instruction.text = "Say the word below";
            
            if (AreTheyEqual(wordToProcess))
            {
                scoreNum += 10;
                currIndex++;
                displayScore();
                displayCorrectWordFromArray();
            }
            else
            {
                instruction.text = "Repeat again";
            }

            startButton.interactable = true;
        }, error =>
        {
            wordUserSay.color = Color.red;
            wordUserSay.text = error;
            startButton.interactable = true;
        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    private string RemoveLeadingSpace(string input)
    {
        if (input.Length > 0 && input[0] == ' ')
        {
            return input.Substring(1);
        }
        else
        {
            return input;
        }
    }

    private bool AreTheyEqual(string word)
    {
        string cleanedWord = Regex.Replace(word, @"[^a-zA-Z0-9 ]", "");
        string correctArray = answerArr[currIndex];
        correctArray = Regex.Replace(correctArray, @"[^a-zA-Z0-9 ]", "");
        //Debug.Log($"This is sentence {correctArray} and this was {cleanedWord}");
        return cleanedWord.ToLower() == correctArray.ToLower();
    }

    private void displayCorrectWordFromArray()
    {
        displayText.text = answerArr[currIndex];
        //Debug.Log($"The current index is {currIndex}");
    }

    private void displayScore()
    {
        score.text = scoreNum.ToString();
    }

    private void SkipButtonClick()
    {
        currIndex++;
        displayCorrectWordFromArray();
    }

    private IEnumerator CountdownCoroutine()
    {
        while (timeRemaining > 0)
        {
            UpdateTimerText();
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        timeRemaining = 0;
        UpdateTimerText();
        SceneManager.LoadScene("GameOver");
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}