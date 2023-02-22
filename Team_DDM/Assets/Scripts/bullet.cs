using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int timer;
    public int bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " hit");
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.takeDamage(bulletDamage);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IDamage>().takeDamage(bulletDamage);
        }

        Destroy(gameObject);

    }
}
