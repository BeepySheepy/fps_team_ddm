using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonFunction : MonoBehaviour
{
    bool isGod;
    public Animator transition;
    public float transitionTime = 1f;
    int currentLevel = 0;
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
            gameManager.instance.playerScript.god = true;
        }
        else
        {
            gameManager.instance.playerScript.god = false;
        }
    }
    public void resume()
    {
        gameManager.instance.unPaused();
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
    }

    public void respawn()
    {
        gameManager.instance.unPaused();
        gameManager.instance.playerScript.respawnPlayer();
    }
    public void nextLevel() //Starts the first Level
    {
        if  (SceneManager.GetActiveScene().buildIndex <= 4)
        {
            gameManager.instance.unPaused();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load next level
            currentLevel += 1;

            #region Saving Data
            //Saving Level
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            PlayerPrefs.SetInt("PlayerHP", gameManager.instance.playerScript.getHP());
            PlayerPrefs.SetFloat("PLayerPosX", gameManager.instance.playerScript.transform.position.x);
            PlayerPrefs.SetFloat("PLayerPosY", gameManager.instance.playerScript.transform.position.y);
            PlayerPrefs.SetFloat("PLayerPosZ", gameManager.instance.playerScript.transform.position.z);
            PlayerPrefs.SetInt("PlayerWeapons", gameManager.instance.playerScript.newGun);
            PlayerPrefs.SetInt("Shotgun Ammo", gameManager.instance.playerScript.fireAmmoCt);
            PlayerPrefs.Save();
            #endregion

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
