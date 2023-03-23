
using UnityEngine;

public class melee : MonoBehaviour
{
    [SerializeField] GameObject meleeObject;
    [SerializeField] int meleeDamage;
    [SerializeField] ParticleSystem meleeAttackFX;
    [SerializeField] enemyAI thisEnemyAIScript;
    
    
    CapsuleCollider meleeHitbox;

    private void Start()
    {
        meleeHitbox = meleeObject.GetComponent<CapsuleCollider>();
        TurnOffMeleeCollider();
    }

    public void TurnOffMeleeCollider()
    {
        Debug.Log("Hitbox turned off");
        meleeHitbox.enabled = false;
    }

    public void TurnOnMeleeCollider()
    {
        
        Debug.Log("Hitbox turned on");
        meleeHitbox.enabled = true;
        Instantiate(meleeAttackFX, meleeHitbox.transform.position, Quaternion.LookRotation(thisEnemyAIScript.GetPlayerDirection()));
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")){
    //        gameManager.instance.playerScript.takeDamage(meleeDamage);
    //    }
    //}
}
