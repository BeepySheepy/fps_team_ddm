using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    [SerializeField] GameObject meleeObject;
    [SerializeField] int meleeDamage;

    CapsuleCollider meleeHitbox;

    private void Start()
    {
        meleeHitbox = meleeObject.GetComponent<CapsuleCollider>();
        TurnOffMeleeCollider();
    }

    public void TurnOffMeleeCollider()
    {
        meleeHitbox.enabled = false;
    }

    public void TurnOnMeleeCollider()
    {
        meleeHitbox.enabled = true;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")){
    //        gameManager.instance.playerScript.takeDamage(meleeDamage);
    //    }
    //}
}
