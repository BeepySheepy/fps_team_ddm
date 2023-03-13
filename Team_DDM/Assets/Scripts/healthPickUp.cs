using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickUp : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.giveHP(HP);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        gameManager.instance.player.GetComponent<AudioSource>().PlayOneShot(pickupSound);
    }
}
