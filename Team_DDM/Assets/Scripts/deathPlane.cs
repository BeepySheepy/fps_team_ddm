using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerSpawn.transform.position = new Vector3(0, 3, 0);
            gameManager.instance.playerScript.setHP(0);
            gameManager.instance.playerDead();
        }
    }
}
