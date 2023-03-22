using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class menuKeyboardFucntions : MonoBehaviour
{
    #region Keyboard Functions
    public GameObject menuButtonStart, playButtonStart, levelButtonStart, optionsButtonStart, creditsButtonStart;

    public GameObject playButtonReturn, levelButtonReturn, optionsButtonReturn, creditsButtonReturn;
    private void Start()
    {
        closePlay();
    }

    //Play Options
    public void openPlay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButtonStart);
    }
    public void closePlay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButtonReturn);
    }

    //Level Options
    public void openLevels()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelButtonStart);
    }
    public void closeLevels()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelButtonReturn);
    }

    //Options Option
    public void openOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButtonStart);
    }
    public void closeOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButtonReturn);
    }

    //Credits Option
    public void openCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsButtonStart);
    }
    public void closeCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsButtonReturn);
    }
    #endregion
}
