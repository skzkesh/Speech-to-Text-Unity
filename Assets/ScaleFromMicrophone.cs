using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public Vector3 minScale, maxScale;
    public TAudioLoudnessDetector detector;

    public float loudnessSensibility = 100f;
    public float threshold = 0.1f;

    private void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        // Debug.Log(loudness);
        if (loudness < threshold) loudness = 0;

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
