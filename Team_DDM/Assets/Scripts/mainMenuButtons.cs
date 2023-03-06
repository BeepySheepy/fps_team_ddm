using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
    GameObject noSavedGameImage;
    public Animator transitions;
    private void Start()
    {
        transitions.SetTrigger("End");
    }
    public void continueGame() //Starts the first Level
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {

        }
        else
        {
           //No Save
        }
    }
    public void newGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();
    }
    public void level1() //Starts the first Level
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1); //Load the spacific scene
    }
    public void level2() //Starts the first Level
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2); //Load the spacific scene
    }
    public void level3() //Starts the first Level
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3); //Load the spacific scene
    }

    public void quit() //Quits the Game
    {
        Application.Quit();
    }

    
}
