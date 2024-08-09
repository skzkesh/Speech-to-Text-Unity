using UnityEngine;

public class VoiceRecorder : MonoBehaviour
{
    private AudioClip recordedAudio;
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    public void StartRecording()
    {
        // Start recording audio using the default microphone
        recordedAudio = Microphone.Start(null, false, 10, 44100);
    }

    public void StopRecording()
    {
        // Stop the recording and get the recorded audio clip
        Microphone.End(null);
        audioSource.clip = recordedAudio;
    }
}