using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickUp : MonoBehaviour
{
    [SerializeField] gunStats gunType;
    [SerializeField] int ammoAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // add health to player
            Destroy(gameObject);
        }
    }
}
