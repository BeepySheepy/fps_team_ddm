using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static gameManager instance;// instance of game manager class

    
    public GameObject player;// player object accesible thorugh other classes
    // add playerScript

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        //add playerScript eventually playerScript does not yet existhttps://open.spotify.com/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
