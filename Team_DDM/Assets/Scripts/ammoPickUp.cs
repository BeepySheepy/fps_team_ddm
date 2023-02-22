using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickUp : MonoBehaviour
{
    [SerializeField] gunStats gunType;
    [SerializeField] bool isFire;
    [SerializeField] bool isIce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFire)
            {
                gameManager.instance.
            }
            Destroy(gameObject);
        }
    }
}
