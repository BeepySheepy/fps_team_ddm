using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour
{
    public void resume()
    {
        gameManager.instance.unPaused();
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
    }
    public void nextLevel() //Starts the first Level
    {
        if  (SceneManager.GetActiveScene().buildIndex <= 4)
        {
            gameManager.instance.unPaused();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); ; //Load next level
        }
    }

    public void mainMenu() //Starts the first Level
    {
        SceneManager.LoadScene(0); //Load the spacific scene
    }

    public void restart()
    {
        gameManager.instance.unPaused();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Application.Quit();
    }
}
