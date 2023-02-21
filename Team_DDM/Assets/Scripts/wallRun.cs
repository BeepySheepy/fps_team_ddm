using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRun : MonoBehaviour
{
    public LayerMask wall;
    public LayerMask ground;
    [SerializeField] float wrSpeed;
    float wrTimeOrig;
    float wrTimer;

    float moveHor;
    float moveVer;

    public float wallDist;
    public float groundDist;
    private RaycastHit leftHit;
    private RaycastHit rightHit;
    private bool wallLeft;
    private bool wallRight;

    private bool exitingRun;
    //public float exitTime;
    public float exitTimer;

    public Transform dir;
    private playerController pc;
    private CapsuleCollider cc;
    private Rigidbody rb;

    private void Start()
    {
        pc = GetComponent<playerController>();
        cc = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        wrTimeOrig = exitTimer;
    }

    private void Update()
    {
        checkForWall();
        wallRunning();
    }

    private void FixedUpdate()
    {
        if (pc.isWallRun)
        {
            if (exitTimer > 0)
            {
                exitTimer -= Time.deltaTime;
                wrMove();
            }
            if (exitTimer <= 0)
            {
                stopRun();
            }
        }
    }

    private void checkForWall()
    {
        wallRight = (Physics.Raycast(transform.position, dir.right, out rightHit, wallDist, wall));
        wallLeft = (Physics.Raycast(transform.position, -dir.right, out leftHit, wallDist, wall));
        if (pc.controller.isGrounded)
        {
            exitTimer = wrTimeOrig;
        }

    }

    private bool isJumping()
    {
        return !Physics.Raycast(transform.position, Vector3.down, groundDist, ground);
    }

    private void wallRunning()
    {
        moveHor = Input.GetAxisRaw("Horizontal");
        moveVer = Input.GetAxisRaw("Vertical");

        if ((wallLeft || wallRight) && moveVer > 0 && isJumping())
        {
            if (!pc.isWallRun)
            {
                startRun();
            }
        }
        else
        {
            if (pc.isWallRun)
            {
                stopRun();
            }
        }
    }

    private void startRun()
    {
        //if (pc.controller.isGrounded)
        //{
        //    exitTimer = wrTimeOrig;
        //}
        pc.isWallRun = true;
    }

    private void wrMove()
    {
        pc.gravity = 0;
        pc.playerVelocity = new Vector3(pc.playerVelocity.x, 0f, pc.playerVelocity.z);
        Vector3 wallNorm = wallRight ? rightHit.normal : leftHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNorm, transform.up);
        rb.AddForce(wallForward * wrSpeed, ForceMode.Force);
    }

    private void stopRun()
    {
        pc.isWallRun = false;
        pc.gravity = pc.gravOrig;
    }

}
