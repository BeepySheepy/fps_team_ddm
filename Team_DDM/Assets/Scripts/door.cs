using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;
    public void Update()
    {
        if (gameManager.instance.doorState == true)
        {
            Debug.Log("Doors Activated");
            door1.SetActive(true);
            door2.SetActive(true);
        }
        else
        {
            //Debug.Log("Doors Deactivated");
            door1.SetActive(false);
            door2.SetActive(false);
        }
    }
}
