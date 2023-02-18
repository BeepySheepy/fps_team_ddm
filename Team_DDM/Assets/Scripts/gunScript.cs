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
    

    public void setGun(gunStats gun)
    {
        mGun = gun;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        GameObject bulletClone = Instantiate(mGun.bullet, mGun.shootPos.position, mGun.bullet.transform.rotation);// create bullet
        if (autoLockOnPlayer)
        {
            bulletClone.GetComponent<Rigidbody>().velocity = shootDirection * mGun.bulletSpeed;
        }
        else
        {
            bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * mGun.bulletSpeed;
        }
        yield return new WaitForSeconds(mGun.fireRate);
        isShooting = false;
    }

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

    public void Reload()
    {
        bulletsInClip = 0;// possible reload while gun still has bullets?
        StartCoroutine(reloadTime());
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public bool IsReloading()
    {
        return isReloading;
    }
    public int GetBulletsInClip()
    {
        return bulletsInClip;
    }

    public void SetShootPos(Transform shootPos)
    {
        mGun.shootPos = shootPos;
    }

}
