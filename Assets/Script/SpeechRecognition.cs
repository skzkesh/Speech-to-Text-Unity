using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeechRecognition : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private TextMeshProUGUI text;

    private AudioClip clip;
    private byte[] bytes;
    private bool recording;
    public UnityEngine.UI.Image appleImage;
    public UnityEngine.UI.Image bananaImage;
    public UnityEngine.UI.Image orangeImage;
    public UnityEngine.UI.Image watermelonImage;
    public UnityEngine.UI.Image strawberryImage;
    private Vector3 initialPosition;

    private void Start()
    {
        startButton.onClick.AddListener(StartRecording);
        stopButton.onClick.AddListener(StopRecording);
        stopButton.interactable = false;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }
    }

    private void StartRecording()
    {
        text.color = Color.white;
        text.text = "Recording...";
        startButton.interactable = false;
        stopButton.interactable = true;
        clip = Microphone.Start(null, false, 10, 44100);
        recording = true;
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
    }

    private void SendRecording()
    {
        text.color = Color.yellow;
        text.text = "Sending...";
        stopButton.interactable = false;
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            text.color = Color.white;
            text.text = response;
            startButton.interactable = true;

            // Check if the response contains the word "apple"
            if (response.ToLower().Contains("apple"))
            {
                // Change the color of the apple image to white
                appleImage.color = Color.white;
            }
            if (response.ToLower().Contains("orange"))
            {
                orangeImage.color = Color.white;
                //orangeImage.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            if (response.ToLower().Contains("banana"))
            {
                bananaImage.color = Color.white;
                //bananaImage.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            if (response.ToLower().Contains("watermelon"))
            {
                watermelonImage.color = Color.white;
                //bananaImage.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            if (response.ToLower().Contains("strawberry"))
            {
                strawberryImage.color = Color.white;
                //bananaImage.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            else
            {
                // Reset the color of the apple image to black
                //appleImage.GetComponent<Rigidbody2D>().isKinematic = true;
                //bananaImage.GetComponent<Rigidbody2D>().isKinematic = true;
                //orangeImage.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }, error => {
            text.color = Color.red;
            text.text = error;
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

    public void toHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
