using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    [SerializeField] Collider meleeDamageHitbox;
    [SerializeField] int meleeDamage;

    private void Start()
    {
        TurnOffMeleeCollider();
    }

    public void TurnOffMeleeCollider()
    {
        meleeDamageHitbox.enabled = false;
    }

    public void TurnOnMeleeCollider()
    {
        meleeDamageHitbox.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            gameManager.instance.playerScript.takeDamage(meleeDamage);
        }
    }
}
