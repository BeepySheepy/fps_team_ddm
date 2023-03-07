using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.setHP(0);
            gameManager.instance.playerDead();
        }
    }
}
