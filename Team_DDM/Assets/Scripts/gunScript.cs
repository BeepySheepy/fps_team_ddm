using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    [SerializeField] gunStats mGun;
    [SerializeField] bool autoLockOnPlayer;
    [SerializeField] bool autoLockOnPlayerY;

    bool isReloading;
    bool isShooting;
    Vector3 shootDirection;
    int bulletsInClip;
    Animator mAnim;
    Transform mShootPos;

    

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
        if (mAnim != null)
        {
            mAnim.SetTrigger("Shoot");
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
        
        if (autoLockOnPlayer)//  locks onto Player
        {
            GameObject bulletClone = Instantiate(mGun.bullet, mShootPos.position, Quaternion.LookRotation(shootDirection));// create bullet
            bulletClone.GetComponent<bullet>().bulletShootInterface(shootDirection, mGun.bulletSpeed);
        }
        else if (autoLockOnPlayerY)// locks onto player Y position
        {
            GameObject bulletClone = Instantiate(mGun.bullet, mShootPos.position, mShootPos.transform.rotation);// create bullet
            bulletClone.GetComponent<bullet>().bulletShootInterface(shootDirection.y, mGun.bulletSpeed);
        }
        else// shoots straight forward
        {
            GameObject bulletClone = Instantiate(mGun.bullet, mShootPos.position, mShootPos.transform.rotation);// create bullet
            bulletClone.GetComponent<bullet>().bulletShootInterface(mGun.bulletSpeed);
        }
    }
    /// <summary>
    /// interface for the IEnumerator(IEnumerators can't pass in parameters)
    /// </summary>
    /// <param name="shootDir">Direction parameter(is skipped if a bool is set in the code)</param>
    public void shootInterface(Vector3 shootDir)
    {
        bulletsInClip--;
        shootDirection = shootDir;// maybe edit this later(mult by new Vector(0,1,0))
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
    /// toggles whether gun is shooting
    /// </summary>
    public void ToggleIsShooting()
    {
        isShooting = !isShooting;
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
        mShootPos = shootPos;
    }
    public void SetAnimator(Animator anim)
    {
        mAnim = anim;
    }

}
