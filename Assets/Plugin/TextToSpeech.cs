using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private async void Start()
    {
        var credentials = new BasicAWSCredentials(accessKey: "", secretKey: "");
        var client = new AmazonPollyClient(credentials, Amazon.RegionEndpoint.APEast1);

        var request = new SynthesizeSpeechRequest()
        {
            Text = "Testing amazon polly, in Unity",
            Engine = Engine.Neural,
            VoiceId = VoiceId.Aria,
            OutputFormat = OutputFormat.Mp3
        };

        SynthesizeSpeechResponse response = await client.SynthesizeSpeechAsync(request);

        WriteIntoFile(response.AudioStream);

        using (var www = UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
        {
            var op = www.SendWebRequest();

            while (!op.isDone) await Task.Yield();

            var clip = DownloadHandlerAudioClip.GetContent(www);

            audioSource.clip = clip;
            audioSource.Play();
        }

    }

    private void WriteIntoFile(Stream stream)
    {
        try
        {
            using (var fileStream = new FileStream($"{Application.persistentDataPath}/audio.mp3", FileMode.Create))
            {
                byte[] buffer = new byte[8 * 1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, offset: 0, count: buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, offset: 0, count: bytesRead);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"File IO error: {e.Message}");
        }
    }
}
