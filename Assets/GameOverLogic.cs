using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] private STT mainscript;
    //int currentScore = mainscript.scoreNum;

    public void openGameScreen()
    {
        SceneManager.LoadScene("BasketWord");
    }
}
