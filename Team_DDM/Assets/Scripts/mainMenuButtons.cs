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
    public void level1() //Starts the first Level
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2); //Load the spacific scene
    }
    public void level2() //Starts the first Level
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3); //Load the spacific scene
    }
    public void level3() //Starts the first Level
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4); //Load the spacific scene
    }

    public void quit() //Quits the Game
    {
        Application.Quit();
    }
}
