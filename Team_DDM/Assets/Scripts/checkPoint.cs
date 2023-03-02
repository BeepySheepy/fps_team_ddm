using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
            gameManager.instance.playerSpawn.transform.position = transform.position;
            //Destroy(gameObject);
        }
    }
}
