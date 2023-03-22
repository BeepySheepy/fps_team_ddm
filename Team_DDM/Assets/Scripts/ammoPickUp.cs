
using UnityEngine;

public class ammoPickUp : MonoBehaviour
{
    [SerializeField] bool isFire;
    [SerializeField] bool isIce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFire)
            {
                gameManager.instance.playerScript.setFireAmmo(4);
            }
            if (isIce)
            {
                gameManager.instance.playerScript.setIceAmmo(2);

            }
            Destroy(gameObject);
        }
    }
}
