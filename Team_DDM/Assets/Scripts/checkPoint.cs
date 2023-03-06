using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
            gameManager.instance.playerScript.checkpointHPTracker();
            gameManager.instance.playerSpawn.transform.position = transform.position;
            //Destroy(gameObject);

        }
    }

}
