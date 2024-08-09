using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillFromMicrophone : MonoBehaviour
{
    public Image audioBar;
    public TAudioLoudnessDetector detector;

    public float loudnessSensibility = 100f;
    public float threshold = 0.1f;

    private void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        //Debug.Log(loudness);
        if (loudness < threshold) loudness = 0.01f;

        audioBar.fillAmount = loudness;
    }
}
