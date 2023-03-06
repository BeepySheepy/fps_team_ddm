using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
    GameObject noSavedGameImage;
    public Animator transitions;
    //public void buttonClicked()
    //{
    //    StartCoroutine(screenTransitioner());
    //}

    //public IEnumerator screenTransitioner()
    //{
    //    transitions.SetTrigger("Start");
    //    yield return new WaitForSeconds(6);
    //    transitions.SetTrigger("End");
    //}

    public void continueGame() //Starts the first Level
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {

        }
        else
        {
            StartCoroutine(noSavedGame());
        }
    }
    public void newGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(6);
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

    IEnumerator noSavedGame()
    {
        noSavedGameImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        noSavedGameImage.SetActive(false);
    }
}
