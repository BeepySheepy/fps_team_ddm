using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] public CharacterController controller;
    public GameObject playerSpawn;
    [SerializeField] Camera mainCamera;

    [Header("----- Player Movement -----")]
    [Range(1, 50)] [SerializeField] int playerSpeed;
    [Range(0, 3)] [SerializeField] int jumpTimes;
    [Range(5, 50)] [SerializeField] int jumpSpeed;
    [Range(5, 150)] [SerializeField] public int gravity;
    [Range(1, 10)] [SerializeField] int HP;
    [SerializeField] float pushBackTime;
    [Header("----- Gun Attributes -----")]
    [SerializeField] public List<gunStats> gunList = new List<gunStats>();
    [Range(0.1f, 5)] [SerializeField] float shootRate;
    [Range(1, 100)] [SerializeField] int shootDist;
    [Range(1, 20)] [SerializeField] int shootDamage;
    [SerializeField] GameObject gunModel;
    [SerializeField] float zoomMax;

    [Header("---- Gun Icons ----")]
    public GameObject pistolIcon;
    public GameObject shotgunIcon;
    public GameObject sniperIcon;

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

    int fireAmmoCt;
    int iceAmmoCt;

    int newGun;
    GameObject levelSpawn;

    bool invuln;
    bool invulnStatus;
    public bool isBurning;
    public bool god;

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
        invulnStatus = false;
        isBurning = false;
        levelSpawn = playerSpawn;
        //respawnPlayer();

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
        if (invulnStatus)
        {
            StartCoroutine(statusInvuln());
        }
    }

    void movement()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = 0;
            jumpsCurrent = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal") + (transform.forward * Input.GetAxis("Vertical")));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && jumpsCurrent < jumpTimes)
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

            if (!isSpread)
            {
                if (selectedGun == 0)
                {
                    GameObject bulletClone = Instantiate(gunList[selectedGun].bullet, transform.position, transform.rotation);
                    bulletClone.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * gunList[selectedGun].bulletSpeed;
                    numShots++;
                    if(gunList[selectedGun].name == ("IceSniper"))
                    {
                        iceAmmoCt--;
                    }
                    yield return new WaitForSeconds(shootRate);
                    isShooting = false;
                }
                else if (selectedGun == 2 && iceAmmoCt > 0)
                {
                    Debug.Log("Fire Shot");
                    GameObject bulletClone = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
                    bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * gunList[selectedGun].bulletSpeed;
                    numShots++;
                    setIceAmmo(-1);
                    yield return new WaitForSeconds(shootRate);
                    isShooting = false;
                }

            }
            else
            {
                Debug.Log("Enters Spread");
                //Quaternion spreadL = Quaternion.Euler(0f, -90, 0f);
                //Quaternion spreadR = Quaternion.Euler(0f, 90, 0f);
                
                if ( fireAmmoCt > 0)
                {
                    Debug.Log("Fire Shot");
                    Vector3 spreadL = new Vector3(gunModel.transform.forward.x, gunModel.transform.forward.y, gunModel.transform.position.z + 1);
                    Vector3 spreadR = new Vector3(gunModel.transform.forward.x, gunModel.transform.forward.y, gunModel.transform.position.z - 1);
                    Debug.Log("Fired 1");
                    GameObject bulletClone1 = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
                    bulletClone1.GetComponent<Rigidbody>().velocity = spreadL * gunList[selectedGun].bulletSpeed;
                    Debug.Log("Fired 2");
                    GameObject bulletClone2 = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
                    bulletClone2.GetComponent<Rigidbody>().velocity = gunModel.transform.forward * gunList[selectedGun].bulletSpeed;
                    Debug.Log("Fired 3");
                    GameObject bulletClone3 = Instantiate(gunList[selectedGun].bullet, transform.position, gunList[selectedGun].bullet.transform.rotation);
                    bulletClone3.GetComponent<Rigidbody>().velocity = spreadR * gunList[selectedGun].bulletSpeed;

                    setFireAmmo(-1);
                }



                numShots++;
                yield return new WaitForSeconds(shootRate);
                isShooting = false;
            }
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
        yield return new WaitForSeconds(5);
        invuln = false;
    }

    IEnumerator statusBurning()
    {
        isBurning = true;
        yield return new WaitForSeconds(2);
        isBurning = false;
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
        isSpread = gunStat.spreadShot;

        Debug.Log("Gun Model Set");
        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        newGun++;

        if (newGun == 1)
        {
            setFireAmmo(4);
        }
        else if (newGun == 2)
        {
            setIceAmmo(2);
        }
        gunIconIndicator(newGun);

    }
    void selectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1)
        {
            selectedGun++;
            Debug.Log(selectedGun);
            changeGun();
            gunIconIndicator(selectedGun);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
        {
            selectedGun--;
            Debug.Log(selectedGun);
            changeGun();
            gunIconIndicator(selectedGun);
        }
    }

    void changeGun()
    {
        shootRate = gunList[selectedGun].fireRate;
        isSpread = gunList[selectedGun].spreadShot;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[selectedGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[selectedGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    }
    public void gunIconIndicator(int selected)
    {

       switch (selected)
       {
           case 0:
               pistolIcon.SetActive(true);
               shotgunIcon.SetActive(false);
               sniperIcon.SetActive(false);
               break;
           case 1:
               pistolIcon.SetActive(false);
               shotgunIcon.SetActive(true);
               sniperIcon.SetActive(false);
               break;
           case 2:
               pistolIcon.SetActive(false);
               shotgunIcon.SetActive(false);
               sniperIcon.SetActive(true);
               break;
       
       }
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
        }
        StartCoroutine(flashHeal());
    }

    public void setHP(int amt)
    {
        HP = amt;
        updatePlayerHPBar();
    }
    IEnumerator flashHeal()
    {
        gameManager.instance.playerHealFlasher.SetActive(true);
        yield return new WaitForSeconds(.2f);
        gameManager.instance.playerHealFlasher.SetActive(false);
    }
    //public void respawnPlayer()
    //{
    //    controller.transform.position.Set(playerSpawn.transform.position.x, playerSpawn.transform.position.y, playerSpawn.transform.position.z);
    //    //Debug.Log($"Respawned at:  + {player.transform.position.x}, {player.transform.position.y}, {player.transform.position.z}");
    //}
    //public void updateSpawn(Transform newSpawn)
    //{
    //    playerSpawn.transform.position = newSpawn.transform.position;
    //    //Debug.Log($"New Spawn:  + {playerSpawn.transform.position.x}, {playerSpawn.transform.position.y}, {playerSpawn.transform.position.z}");
    //}
}
