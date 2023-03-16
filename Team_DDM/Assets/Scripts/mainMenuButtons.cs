using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject noSavedGameImage;
    [SerializeField] GameObject deletedSavedGameImage;
    public void continueGame() //Starts the first Level
    {
        if (PlayerPrefs.HasKey("Saved Level"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(PlayerPrefs.GetInt("Saved Level")); //Load the spacific scene
        }
        else
        {
            //No Save
            StartCoroutine(noSave());
        }
    }

    IEnumerator noSave()
    {
        noSavedGameImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        noSavedGameImage.SetActive(false);
    }
    public void newGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Saved Level", SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void deleteSave()
    {
        PlayerPrefs.DeleteKey("Saved Level");
        StartCoroutine(saveDeleted());
    }
    IEnumerator saveDeleted()
    {
        deletedSavedGameImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        deletedSavedGameImage.SetActive(false);
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
