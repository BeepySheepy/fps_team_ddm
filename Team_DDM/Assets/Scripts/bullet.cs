using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int timer;
    [SerializeField] bool homingBullet;
    public int bulletDamage;

    Vector3 mShootDirection;
    int mBulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
        mShootDirection = Vector3.zero;// base set to forward movement
    }

    void Update()// will be use for homing bullet
    {
        if (homingBullet)// bullet follows 
        {
            bulletShootInterface(gameManager.instance.player.transform.position - transform.position, mBulletSpeed);
        }
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
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        Destroy(gameObject);

    }

    /// <summary>
    /// interface for the moved bullet whose velocity is defined within itself
    /// </summary>
    /// <param name="shootDirection">direction to multiply the vector by</param>
    public void bulletShootInterface(Vector3 shootDirection, int bulletSpeed)
    {
        mShootDirection = shootDirection;// set mShootDirection
        mBulletSpeed = bulletSpeed;
        bulletShootVector();
    }
    /// <summary>
    /// interface for the moved bullet whose velocity is defined within itself
    /// </summary>
    public void bulletShootInterface(int bulletSpeed)// overload
    {
        mBulletSpeed = bulletSpeed;
        bulletShootVector();// the overload without a inputted vector send the user straight into the non-interface version
    }
    /// <summary>
    /// the private function that controls the bullet movement
    /// </summary>
    void bulletShootVector()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * mBulletSpeed;
    }
}
