using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    private float delayTime = 1f;

    private void Start()
    {
        Invoke("MoveOpening", delayTime);
    }

    private void MoveOpening()
    {
        SceneManager.LoadScene("Opening");
    }
}
