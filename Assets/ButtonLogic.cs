using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public void openGameScreen()
    {
        SceneManager.LoadScene("BasketWord");
    }
}
