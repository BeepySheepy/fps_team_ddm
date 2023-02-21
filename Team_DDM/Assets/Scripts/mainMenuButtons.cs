using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
    public void play() //Starts the first Level
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1); //Load the spacific scene
    }

    public void quit() //Quits the Game
    {
        Application.Quit();
    }
}
