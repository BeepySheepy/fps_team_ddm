using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [Range(1, 10)] [SerializeField] int playerSpeed;
    [Range(0, 3)] [SerializeField] int jumpTimes;
    [Range(5, 50)] [SerializeField] int jumpSpeed;
    [Range(5, 150)] [SerializeField] int gravity;
    [Range(0.1f, 2.5f)] [SerializeField] float shootRate;
    [Range(1, 200)] [SerializeField] int shootDist;
    [Range(1, 10)] [SerializeField] int shootDamage;

    int jumpsCurrent;
    bool isShooting;
    Vector3 move;
    Vector3 playerVelocity;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement();
        if (!isShooting && Input.GetButton("Shoot"))
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
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator shoot()
    {
        isShooting = true;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {

        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
