using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuButtonNoise : MonoBehaviour
{
    public AudioSource buttonNoise;
    float menuMusicVol;
    // Start is called before the first frame update
    void Start()
    {
        menuMusicVol = gameManager.instance.MusicVol;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPlayer()
    {
        buttonNoise.Play(); //Make it play based on vol
    }
}
