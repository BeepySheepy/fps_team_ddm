using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    [Header("-----Navigation-----")]
    [SerializeField] NavMeshAgent navMesh;// allows for movement
    [SerializeField] Transform headPos;// tracks head pos instead of from (0,0)
    [Range(1, 10)][SerializeField] int enemyTurnSpeed;
    [Header("-----Vision-----")]
    [Range(1, 50)][SerializeField] float visionDistance;
    [Range(1, 50)][SerializeField] float visionAngle;
    [Header("-----Gun-----")]
    [SerializeField] gunScript gun;
    [SerializeField] int dropHP;
    [Header("-----Melee-----")]
    
    [SerializeField] float meleeTimer;

    [SerializeField] bool permaAggro;
    [SerializeField] int enemyTypeID;


    bool playerInRange;// bool if the player is within the range of detection of the enemy
    Vector3 playerDirection;
    float angleTowardsPlayer;
    Animator anim;
    bool isInMelee;

    // Start is called before the first frame update
    void Start()
    {

        gun.SetShootPos(headPos);
        anim = GetComponent<Animator>();
        navMesh.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh.isActiveAndEnabled)
        {
            if (anim != null)
            {
                anim.SetFloat("Speed", navMesh.velocity.normalized.magnitude);
            }
            

            playerDirection = gameManager.instance.player.transform.position - headPos.position;// creates a vector between the player and the enemy
            if (playerInRange || playerInVisualRange() || permaAggro)
            {
                navMesh.SetDestination(gameManager.instance.player.transform.position);// sets enemy destination as the player
                if (navMesh.remainingDistance < navMesh.stoppingDistance)// enemy is closer than nav mesh stopping distance
                {
                    facePlayer();
                }

                if(gun != null)
                {
                    // gun stuff
                    if (!gun.IsShooting() && gun.GetBulletsInClip() != 0)
                    {
                        Debug.Log("Enemy Shooting");
                        gun.shootInterface(playerDirection);
                    }
                    else if (!gun.IsShooting() && !gun.IsReloading() && gun.GetBulletsInClip() == 0)// reload
                    {
                        Debug.Log("Enemy Reloading");
                        gun.Reload();
                    }
                }
                else// melee system
                {
                    if (!isInMelee)
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
            Debug.DrawRay(headPos.position, playerDirection);
            RaycastHit hit;
            if (Physics.Raycast(headPos.position, playerDirection, out hit))//Raycast hit's something
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
        yield return new WaitForSeconds(meleeTimer);
        isInMelee = false;
    }

    public Transform GetHeadPos()
    {
        return headPos;
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
}
