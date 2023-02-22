using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonFunction : MonoBehaviour
{
    bool isGod;
    private void Start()
    {
        isGod = false;
    }
    private void Update()
    {
        godCheck();
    }
    public void invincibilityOn()
    {
        isGod = !isGod;
        
    }
    public void godCheck()
    {
        if (isGod)
        {
            gameManager.instance.playerScript.setHP(9999);
        }
        else
        {
            gameManager.instance.playerScript.setHP(8);
        }
    }
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
