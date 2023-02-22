using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    [SerializeField] gunStats mGun;
    [SerializeField] bool autoLockOnPlayer;

    bool isReloading;
    bool isShooting;
    Vector3 shootDirection;
    int bulletsInClip;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// basic gun setter
    /// </summary>
    /// <param name="gun">passed in gunStats parameter</param>
    public void setGun(gunStats gun)
    {
        mGun = gun;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        if (anim != null)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            createBullet();
        }
        yield return new WaitForSeconds(mGun.fireRate);
        isShooting = false;
    }

    void createBullet()
    {
        GameObject bulletClone = Instantiate(mGun.bullet, mGun.shootPos.position, mGun.bullet.transform.rotation);// create bullet
        if (autoLockOnPlayer)//  locks onto Player
        {
            bulletClone.GetComponent<Rigidbody>().velocity = shootDirection * mGun.bulletSpeed;
        }
        else// shoots straight forward
        {
            bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * mGun.bulletSpeed;
        }
    }
    /// <summary>
    /// interface for the IEnumerator(IEnumerators can't pass in parameters)
    /// </summary>
    /// <param name="shootDir">Direction parameter(is skipped if a bool is set in the code)</param>
    public void shootInterface(Vector3 shootDir)
    {
        bulletsInClip--;
        shootDirection = shootDir;
        StartCoroutine(shoot());
    }

    IEnumerator reloadTime()
    {
        isReloading = true;
        yield return new WaitForSeconds(mGun.reloadSpeed);
        bulletsInClip = mGun.clipSize;
        isReloading = false;
    }
    /// <summary>
    /// interface for reload
    /// </summary>
    public void Reload()
    {
        bulletsInClip = 0;// possible reload while gun still has bullets?
        StartCoroutine(reloadTime());
    }
    /// <summary>
    /// tracks if shooting internally
    /// </summary>
    /// <returns>if the gun is shooting</returns>
    public bool IsShooting()
    {
        return isShooting;
    }
    /// <summary>
    /// tracks if reloading internally
    /// </summary>
    /// <returns>if the gun is reloading</returns>
    public bool IsReloading()
    {
        return isReloading;
    }
    /// <summary>
    /// tracks the number of bullets in clip internally(amount of ammo remaining not tracked in script)
    /// </summary>
    /// <returns>number of bullets left in the clip</returns>
    public int GetBulletsInClip()
    {
        return bulletsInClip;
    }
    /// <summary>
    /// Should be called when setting up a new gun on a character
    /// </summary>
    /// <param name="shootPos">the transform for the shoot position for the player or enemy</param>
    public void SetShootPos(Transform shootPos)
    {
        mGun.shootPos = shootPos;
    }

}
