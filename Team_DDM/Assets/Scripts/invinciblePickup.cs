using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invinciblePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.callInvuln();
            Destroy(gameObject);
        }
    }
}
