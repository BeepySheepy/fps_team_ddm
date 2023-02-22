using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] public CharacterController controller;

    [Header("----- Player Movement -----")]
    [Range(1, 50)] [SerializeField] int playerSpeed;
    [Range(0, 3)] [SerializeField] int jumpTimes;
    [Range(5, 50)] [SerializeField] int jumpSpeed;
    [Range(5, 150)] [SerializeField] public int gravity;
    [Range(1, 10)] [SerializeField] int HP;
    [SerializeField] float pushBackTime;
    [Header("----- Gun Attributes -----")]
    [SerializeField] List<gunStats> gunList = new List<gunStats>();
    [Range(0.1f, 5)] [SerializeField] float shootRate;
    [Range(1, 100)] [SerializeField] int shootDist;
    [Range(1, 20)] [SerializeField] int shootDamage;
    [SerializeField] GameObject gunModel;
    [SerializeField] float zoomMax;

    int jumpsCurrent;
    public int speedOrig;
    public int gravOrig;
    bool isShooting;
    public bool isWallRun;
    public int wallRunSpeed;
    int selectedGun;
    float zoomOrig;
    Vector3 move;
    public Vector3 playerVelocity;
    int HPOrig;
    Vector3 pushBack;


    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;
        gravOrig = gravity;
        speedOrig = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        pushBack = Vector3.Lerp(pushBack, Vector3.zero, Time.deltaTime * pushBackTime);
        movement();
        if (!isShooting && Input.GetButton("Shoot") && gunList.Count > 0)
        {
            StartCoroutine(shoot());
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
        isShooting = true;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {
            Debug.Log(hit.collider.name + " hit");
            
            if (hit.collider.GetComponent<IDamage>() != null)
            {
                hit.collider.GetComponent<IDamage>().takeDamage(shootDamage);
            }
        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        StartCoroutine(flashDamage());
        updatePlayerHPBar();

        if (HP <= 0)
        {
            gameManager.instance.playerDead();
        }
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

    }
    void selectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1)
        {
            selectedGun++;
            changeGun();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
        {
            selectedGun--;
            changeGun();
        }
    }

    void changeGun()
    {
        shootRate = gunList[selectedGun].fireRate;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[selectedGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[selectedGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void pushBackDir(Vector3 dir)
    {
        Debug.Log("Push Back Go");
        pushBack += dir;
    }
}
