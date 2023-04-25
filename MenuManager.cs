using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //play music
    public void MenuJump()
    {
        SceneManager.LoadScene(1); // go to how to play

    }
    public void Startgame()
    {
        SceneManager.LoadScene(2); // go to how to play
    }

    public void StartTime()
    {
        GameManager.instance.isTimeStart = true;
    }
}
