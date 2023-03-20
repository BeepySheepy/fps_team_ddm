
using UnityEngine;

public class melee : MonoBehaviour
{
    [SerializeField] GameObject meleeObject;
    [SerializeField] int meleeDamage;
    [SerializeField] ParticleSystem meleeAttackFX;
    
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
        Instantiate(meleeAttackFX, meleeHitbox.transform.position,
            new Quaternion(meleeAttackFX.transform.rotation.x, (meleeHitbox.transform.rotation.y + .5f), meleeHitbox.transform.rotation.z,
            meleeHitbox.transform.rotation.w));
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")){
    //        gameManager.instance.playerScript.takeDamage(meleeDamage);
    //    }
    //}
}
