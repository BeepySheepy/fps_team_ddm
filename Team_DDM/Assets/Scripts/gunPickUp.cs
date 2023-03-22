
using UnityEngine;

public class gunPickUp : MonoBehaviour
{
    [SerializeField] gunStats gun;
    [SerializeField] Transform shootPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            //gun.shootPos = shootPosition;
            gameManager.instance.playerScript.gunPick(gun);
            Destroy(gameObject);
        }
    }
}
