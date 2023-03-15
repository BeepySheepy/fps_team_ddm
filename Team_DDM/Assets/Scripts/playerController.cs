using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine;


public class playerController : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] public CharacterController controller;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject fireEffect;
    [SerializeField] AudioClip fireSoundEffect;

    [Header("----- Player Movement -----")]
    [Range(1, 50)][SerializeField] int playerSpeed;
    [Range(0, 3)][SerializeField] int jumpTimes;
    [Range(5, 50)][SerializeField] int jumpSpeed;
    [Range(5, 150)][SerializeField] public int gravity;
    [Range(1, 10)][SerializeField] int HP;
    [SerializeField] float pushBackTime;
    [SerializeField] float coyoteTimer;

    [Header("----- Gun Attributes -----")]
    [SerializeField] public List<gunStats> gunList = new List<gunStats>();
    [Range(0.1f, 5)][SerializeField] float shootRate;
    [Range(1, 100)][SerializeField] int shootDist;
    [Range(1, 20)][SerializeField] int shootDamage;
    [SerializeField] GameObject gunModel;
    [SerializeField] GameObject shootPosition;
    [SerializeField] GameObject light;
    [SerializeField] float zoomMax;

    int jumpsCurrent;
    public int speedOrig;
    public int gravOrig;
    bool isShooting;
    bool isReloading;
    public bool isWallRun;
    public int wallRunSpeed;
    int selectedGun;
    float zoomOrig;
    Vector3 move;
    public Vector3 playerVelocity;
    int HPOrig;
    Vector3 pushBack;
    int numShots;
    bool isSpread;
    bool canJump;
    bool coyoteTimeBool;
    public int fireAmmoCt;
    int iceAmmoCt;

    public int newGun;
    //levelSpawn needs fix
    GameObject levelSpawn;

    bool invuln;
    public bool isBurning;
    public bool god;

    //CheckPoint Info
    int checkpointHP;
    int checkpointAmmoF;
    int checkpointAmmoI;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        HPOrig = HP;
        gravOrig = gravity;
        speedOrig = playerSpeed;
        numShots = 0;
        isReloading = false;
        fireAmmoCt = 0;
        iceAmmoCt = 0;
        newGun = -1;
        invuln = false;
        isBurning = false;
        checkpointHP = HPOrig;
        levelSpawn = gameManager.instance.playerSpawn;
        spawnPlayer();
        controller.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        pushBack = Vector3.Lerp(pushBack, Vector3.zero, Time.deltaTime * pushBackTime);
        movement();
        selectGun();
        if (!isShooting && !isReloading && Input.GetButton("Shoot") && gunList.Count > 0)
        {
            Debug.Log("Update Func Working.");
            StartCoroutine(shoot());
        }
        if (isBurning)
        {
            StartCoroutine(burnTick());
        }
    }

    void movement()
    {
        if (controller.isGrounded)// character grounded
        {
            coyoteTimeBool = true;
            canJump = true;
            playerVelocity.y = 0;
            jumpsCurrent = 0;
        }
        else if (coyoteTimeBool)
        {
            StartCoroutine(CoyoteTimer());
        }

        move = (transform.right * Input.GetAxis("Horizontal") + (transform.forward * Input.GetAxis("Vertical")));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && jumpsCurrent < jumpTimes && canJump)
        {
            jumpsCurrent++;
            playerVelocity.y = jumpSpeed;
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move((playerVelocity + pushBack) * Time.deltaTime);


        if (isWallRun)
        {
            playerSpeed = wallRunSpeed;
        }
        else
        {
            playerSpeed = speedOrig;
            gravity = gravOrig;
        }
    }

    IEnumerator shoot()
    {
        Debug.Log("Enters IE");
        if (numShots < gunList[selectedGun].clipSize)
        {
            Debug.Log("Enters if statement");
            isShooting = true;

            //RaycastHit hit;
            //if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
            //{
            //    Debug.Log(hit.collider.name + " hit");
            //    
            //    if (hit.collider.GetComponent<IDamage>() != null)
            //    {
            //        hit.collider.GetComponent<IDamage>().takeDamage(shootDamage);
            //    }
            //}

            GameObject bulletClone = Instantiate(gunList[selectedGun].bullet, shootPosition.transform.position, shootPosition.transform.rotation);
            bulletClone.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * gunList[selectedGun].bulletSpeed;
            numShots++;
            yield return new WaitForSeconds(shootRate);
            isShooting = false;
            //if (!isSpread)
            //{
            //    if (selectedGun == 0)
            //    {
            //        GameObject bulletClone = Instantiate(gunList[selectedGun].bullet, transform.position, transform.rotation);
            //        bulletClone.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * gunList[selectedGun].bulletSpeed;
            //        numShots++;
            //        yield return new WaitForSeconds(shootRate);
            //        isShooting = false;
            //    }
            //    else if (selectedGun == 2 && iceAmmoCt > 0)
            //    {
            //        Debug.Log("Fire Shot");
            //        GameObject bulletClone = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
            //        bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * gunList[selectedGun].bulletSpeed;
            //        numShots++;
            //        setIceAmmo(-1);
            //        yield return new WaitForSeconds(shootRate);
            //        isShooting = false;
            //    }
            //
            //}
            //else
            //{
            //    Debug.Log("Enters Spread");
            //    //Quaternion spreadL = Quaternion.Euler(0f, -90, 0f);
            //    //Quaternion spreadR = Quaternion.Euler(0f, 90, 0f);
            //
            //    if (fireAmmoCt > 0)
            //    {
            //        Debug.Log("Fire Shot");
            //        Vector3 spreadL = new Vector3(gunModel.transform.forward.x, gunModel.transform.forward.y, gunModel.transform.position.z + 1);
            //        Vector3 spreadR = new Vector3(gunModel.transform.forward.x, gunModel.transform.forward.y, gunModel.transform.position.z - 1);
            //        Debug.Log("Fired 1");
            //        GameObject bulletClone1 = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
            //        bulletClone1.GetComponent<Rigidbody>().velocity = spreadL * gunList[selectedGun].bulletSpeed;
            //        Debug.Log("Fired 2");
            //        GameObject bulletClone2 = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
            //        bulletClone2.GetComponent<Rigidbody>().velocity = gunModel.transform.forward * gunList[selectedGun].bulletSpeed;
            //        Debug.Log("Fired 3");
            //        GameObject bulletClone3 = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
            //        bulletClone3.GetComponent<Rigidbody>().velocity = spreadR * gunList[selectedGun].bulletSpeed;
            //
            //        setFireAmmo(-1);
            //    }
            //
            //
            //
            //    numShots++;
            //    yield return new WaitForSeconds(shootRate);
            //    isShooting = false;
            //}
        }
        else
        {
            StartCoroutine(reload());
        }
    }

    IEnumerator reload()
    {
        isReloading = true;
        numShots = 0;
        gameManager.instance.reloadDisplay(true);
        yield return new WaitForSeconds(gunList[selectedGun].reloadSpeed);
        isReloading = false;
        gameManager.instance.reloadDisplay(false);
    }

    public void takeDamage(int dmg)
    {
        if (!invuln && !god)
        {
            StartCoroutine(flashDamage());
            HP -= dmg;
            updatePlayerHPBar();

            if (HP <= 0)
            {
                gameManager.instance.playerDead();
            }

            StartCoroutine(IFrames());
        }
    }
    public void takeFireDamage(int dmg)
    {
        StartCoroutine(flashDamage());
        if (!invuln && !god)
        {
            takeDamage(dmg);
            StartCoroutine(statusBurning());
        }
    }
    IEnumerator IFrames()
    {
        invuln = true;
        yield return new WaitForSeconds(0.5f);
        invuln = false;
    }

    IEnumerator statusInvuln()
    {
        invuln = true;
        yield return new WaitForSeconds(3);
        invuln = false;
    }
    public void callInvuln()
    {
        StartCoroutine(statusInvuln());
    }

    IEnumerator statusBurning()
    {
        fireEffect.SetActive(true);
        audioSource.PlayOneShot(fireSoundEffect);
        isBurning = true;
        yield return new WaitForSeconds(2);
        isBurning = false;
        fireEffect.SetActive(false);
    }
    IEnumerator burnTick()
    {
        takeDamage(1);
        yield return new WaitForSeconds(1);
    }

    IEnumerator flashDamage()
    {
        gameManager.instance.playerDamageFlasher.SetActive(true);
        yield return new WaitForSeconds(.2f);
        gameManager.instance.playerDamageFlasher.SetActive(false);
    }

    public void updatePlayerHPBar()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / (float)HPOrig;
    }

    public void gunPick(gunStats gunStat)
    {
        Debug.Log("Got" + gunStat);
        gunList.Add(gunStat);

        shootRate = gunStat.fireRate;

        Debug.Log("Gun Model Set");
        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        light.SetActive(true);
        newGun++;
        gameManager.instance.gunIconIndicator(newGun);

    }
    void selectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1)
        {
            selectedGun++;
            Debug.Log(selectedGun);
            changeGun();
            gameManager.instance.gunIconIndicator(selectedGun);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
        {
            selectedGun--;
            Debug.Log(selectedGun);
            changeGun();
            gameManager.instance.gunIconIndicator(selectedGun);
        }
    }

    void changeGun()
    {
        shootRate = gunList[selectedGun].fireRate;
        isSpread = gunList[selectedGun].spreadShot;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[selectedGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[selectedGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void pushBackDir(Vector3 dir)
    {
        Debug.Log("Push Back Go");
        pushBack += dir;
    }
    public void setFireAmmo(int amt)
    {
        fireAmmoCt += amt;
        gameManager.instance.ammoUpdaterF(fireAmmoCt);
    }
    public void setIceAmmo(int amt)
    {
        iceAmmoCt += amt;
        gameManager.instance.ammoUpdaterI(iceAmmoCt);
    }
    public int getFireAmmo()
    {
        return fireAmmoCt;
    }
    public int geticeAmmo()
    {
        return iceAmmoCt;
    }
    public int getSelectedGun()
    {
        return selectedGun;
    }
    public void giveHP(int amt)
    {
        if (HP + amt <= HPOrig)
        {
            HP += amt;
            updatePlayerHPBar();
        }
        else
        {
            HP = HPOrig;
            updatePlayerHPBar();
        }
        StartCoroutine(flashHeal());
    }

    public void setHP(int amt)
    {
        HP = amt;
        updatePlayerHPBar();
    }
    public int getHP()
    {
        return HP;
    }
    IEnumerator flashHeal()
    {
        gameManager.instance.playerHealFlasher.SetActive(true);
        yield return new WaitForSeconds(.2f);
        gameManager.instance.playerHealFlasher.SetActive(false);
    }
    public void spawnPlayer()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawn.transform.position;
        HP = HPOrig;
        updatePlayerHPBar();
        controller.enabled = true;
    }

    public void respawnPlayer()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawn.transform.position;
        setHP(checkpointHP);
        setFireAmmo(checkpointAmmoF);
        setIceAmmo(checkpointAmmoI);
        controller.enabled = true;
    }

    public void checkpointHPTracker()
    {
        checkpointHP = HP;
        checkpointAmmoF = fireAmmoCt;
        checkpointAmmoI = iceAmmoCt;
    }

    IEnumerator CoyoteTimer()
    {
        coyoteTimeBool = false;
        yield return new WaitForSeconds(coyoteTimer);
        canJump = false;
    }

    

}
