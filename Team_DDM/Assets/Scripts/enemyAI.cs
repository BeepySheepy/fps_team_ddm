using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    [Header("-----Navigation-----")]
    [SerializeField] NavMeshAgent navMesh;// allows for movement
    [SerializeField] Transform[] headPos;// tracks head pos instead of from (0,0)
    [Range(1, 10)][SerializeField] int enemyTurnSpeed;
    [SerializeField] ParticleSystem footStepParticle;
    [SerializeField] AudioClip[] footstepSounds;
    [SerializeField] Transform leftFoot;
    [SerializeField] Transform rightFoot;
    [SerializeField] bool enemyActivated;// allows enemies to start deactivated so particle effect can finish
    [Header("-----Vision-----")]
    [Range(1, 50)][SerializeField] float visionDistance;
    [Range(1, 50)][SerializeField] float visionAngle;
    [Header("-----Gun-----")]
    [SerializeField] gunScript gun;
    [SerializeField] float plusYAimDir;
    [Header("-----Melee-----")]
    [SerializeField] float meleeTimer;
    [SerializeField] float meleeRange;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip meleeAttackSound;

    [SerializeField] bool permaAggro;
    [SerializeField] int enemyTypeID;


    bool playerInRange;// bool if the player is within the range of detection of the enemy
    Vector3 playerDirection;
    float angleTowardsPlayer;
    Animator anim;
    bool isInMelee;
    int shootPosIter;

    // Start is called before the first frame update
    void Start()
    {
        shootPosIter = 0;

        anim = GetComponent<Animator>();
        if (anim != null && enemyTypeID != (int)enemies.bulletHell && gun != null)
        {
            gun.SetAnimator(anim);
        }
        navMesh.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyActivated)
        {
            if (navMesh.isActiveAndEnabled)
            {
                if (anim != null)
                {
                    anim.SetFloat("Speed", navMesh.velocity.normalized.magnitude);// movement animation
                }


                FindPlayerDirection();
                if (playerInRange || playerInVisualRange() || permaAggro)
                {
                    navMesh.SetDestination(gameManager.instance.player.transform.position);// sets enemy destination as the player
                    if (navMesh.remainingDistance < navMesh.stoppingDistance)// enemy is closer than nav mesh stopping distance
                    {
                        facePlayer();
                    }

                    if (gun != null && !gun.IsShooting() && gun.GetBulletsInClip() != 0)// shoot
                    {

                        for (shootPosIter = 0; shootPosIter < headPos.Length; shootPosIter++)
                        {
                            Debug.Log(shootPosIter);
                            // gun stuff
                            gun.SetShootPos(headPos[shootPosIter]);
                            FindPlayerDirection();

                            Debug.Log("Enemy Shooting");
                            gun.shootInterface(playerDirection);// figure out a way to change playerDirection between shootPos changes

                        }
                        shootPosIter = 0;// reset shootPosIter
                    }
                    else if (gun != null && !gun.IsShooting() && !gun.IsReloading() && gun.GetBulletsInClip() <= 0)// reload
                    {
                        Debug.Log("Enemy Reloading");
                        gun.Reload();
                    }
                    else if (gun == null && navMesh.remainingDistance < (navMesh.stoppingDistance + meleeRange) && !isInMelee)// melee system
                    {
                        StartCoroutine(melee());

                    }
                }
            }
        }
    }

    /// <summary>
    /// enemy turns to face the player
    /// </summary>
    void facePlayer()
    {
        //playerDirection moved to Update
        Vector3 dupPlayerDir = playerDirection;
        dupPlayerDir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dupPlayerDir);// define quaternion
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * enemyTurnSpeed);
    }

    /// <summary>
    /// Player enters the enemies sphere collider
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered An Enemy's Range");
            playerInRange = true;
        }
    }
    /// <summary>
    /// player exits the enemies sphere collider
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exited An Enemy's Range");
            playerInRange = false;
        }
    }

    bool playerInVisualRange()
    {

        float playerDistance = Mathf.Sqrt(Mathf.Pow(playerDirection.x, 2) + Mathf.Pow(playerDirection.z, 2));// distance from x & z values
        //Debug.Log(playerDistance);

        if (playerDistance < visionDistance)// player is close enough to see
        {
            angleTowardsPlayer = Vector3.Angle(new Vector3(playerDirection.x, 0, playerDirection.z), transform.forward);
            Debug.DrawRay(headPos[0].position, playerDirection);// headPos[0] should always be the forward facing position of the enemy
            RaycastHit hit;
            if (Physics.Raycast(headPos[0].position, playerDirection, out hit))//Raycast hit's something
            {
                if (hit.collider.CompareTag("Player") && angleTowardsPlayer <= visionAngle)// Raycast hits player while
                {
                    return true;
                }
            }
        }

        return false;// player not close enough to see
    }

    public int GetEnemyTypeID()
    {
        return enemyTypeID;
    }

    public void TurnOffNavMesh()
    {
        navMesh.enabled = false;
    }



    IEnumerator melee()
    {
        isInMelee = true;
        anim.SetTrigger("Melee");
        if(source != null)
        {
            source.PlayOneShot(meleeAttackSound);
        }
        yield return new WaitForSeconds(meleeTimer);
        isInMelee = false;
    }

    public Transform GetHeadPos()
    {
        return headPos[shootPosIter];
    }


    public void flipAgent()
    {
        navMesh.enabled = !navMesh.enabled;
    }
    public bool getAgent()
    {
        if (navMesh.isActiveAndEnabled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void rightFootStepParticleEffect()
    {
        PlayFootstepNoise();
        Debug.Log("Right Foot Step");
        Instantiate(footStepParticle, rightFoot);
    }

    void leftFootStepParticleEffect()
    {
        PlayFootstepNoise();
        Debug.Log("Left Foot Step");
        Instantiate(footStepParticle, leftFoot);
    }

    void FindPlayerDirection()
    {
        playerDirection = gameManager.instance.player.transform.position - headPos[shootPosIter].position;// creates a vector between the player and the enemy
        playerDirection.y += plusYAimDir;
    }

    public void Activate()
    {
        enemyActivated = true;
    }
    public void Deactivate()
    {
        enemyActivated = false;
    }
    void PlayFootstepNoise()
    {
        if(footstepSounds.Length > 0)
        {
            source.PlayOneShot(footstepSounds[Random.Range(0,footstepSounds.Length-1)]);// random footstep noise
        }
    }
}
