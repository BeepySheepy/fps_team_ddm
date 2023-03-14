using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int timer;
    [SerializeField] bool homingBullet;
    [SerializeField] float homingBulletTurnSpeed;
    [SerializeField] ParticleSystem bulletHitEffect;
    public int bulletDamage;

    Vector3 mShootDirection;
    int mBulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
        mShootDirection = transform.forward;// base set to forward movement
    }

    void Update()// will be use for homing bullet
    {
        if (homingBullet)// bullet follows 
        {
            homing();// locks on player
            bulletShootInterface(transform.forward, mBulletSpeed);
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
        else if (other.CompareTag("Switch"))
        {
            other.GetComponent<IDamage>().takeDamage(bulletDamage);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if(bulletHitEffect != null)
        {
            Instantiate(bulletHitEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);

    }

    /// <summary>
    /// interface for the moved bullet whose velocity is defined within itself
    /// </summary>
    /// <param name="shootDirection">direction to multiply the vector by</param>
    /// <param name="bulletSpeed">speed of the bullet</param>
    public void bulletShootInterface(Vector3 shootDirection, int bulletSpeed)
    {
        mShootDirection = shootDirection;// set mShootDirection
        mBulletSpeed = bulletSpeed;
        bulletShootVector();
    }/// <summary>
     /// interface for the moved bullet whose velocity is defined within itself
     /// </summary>
     /// <param name="shootDirectionY">the Y float value of the shootDirection vector</param>
     /// <param name="bulletSpeed">speed of the bullet</param>
    public void bulletShootInterface(float shootDirectionY, int bulletSpeed)
    {
        mShootDirection = transform.forward;// set mShootDirection
        mShootDirection.y = shootDirectionY;
        mBulletSpeed = bulletSpeed;
        bulletShootVector();
    }
    /// <summary>
    /// interface for the moved bullet whose velocity is defined within itself
    /// </summary>
    /// <param name="bulletSpeed">speed of the bullet</param>
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
        GetComponent<Rigidbody>().velocity = mShootDirection * mBulletSpeed;
    }

    void homing()
    {
        mShootDirection = gameManager.instance.player.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(mShootDirection);// define quaternion
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * homingBulletTurnSpeed);
    }
}
