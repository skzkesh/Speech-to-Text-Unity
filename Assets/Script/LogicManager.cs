using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public void toKnowledgeTreeScene()
    {
        SceneManager.LoadScene("Knowledge");
    }

    public void toBasketFruitScene()
    {
        SceneManager.LoadScene("BasketOption");
    }

    public void toBasketWord()
    {
        SceneManager.LoadScene("Opening");
    }

    public void toBasketSentence()
    {
        SceneManager.LoadScene("BasketSentence");
    }

    public void toBasketPassage()
    {
        SceneManager.LoadScene("BasketPassage");
    }
}


