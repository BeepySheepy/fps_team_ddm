using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Movement -----")]
    [Range(1, 10)] [SerializeField] int playerSpeed;
    [Range(0, 3)] [SerializeField] int jumpTimes;
    [Range(5, 50)] [SerializeField] int jumpSpeed;
    [Range(5, 150)] [SerializeField] int gravity;
    [Range(1, 10)] [SerializeField] int HP;
    [Header("----- Gun Attributes -----")]
    [Range(0.1f, 2.5f)] [SerializeField] float shootRate;
    [Range(1, 200)] [SerializeField] int shootDist;
    [Range(1, 10)] [SerializeField] int shootDamage;
    [SerializeField] int bulletSpeed;

    int jumpsCurrent;
    bool isShooting;
    Vector3 move;
    Vector3 playerVelocity;

    //My Work
    int HPOrig;


    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        if (!isShooting && Input.GetButton("Shoot"))
        {
            StartCoroutine(shoot());
        }
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.white, 20);
        //Debug.DrawLine(move, playerVelocity);
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
        controller.Move(playerVelocity * Time.deltaTime);
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
}
