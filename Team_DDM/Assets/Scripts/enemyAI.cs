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
    [SerializeField] int clipSize;
    [SerializeField] GameObject bullet;
    [SerializeField] int bulletSpeed;
    [SerializeField] float fireRate;
    [SerializeField] float reloadSpeed;


    bool playerInRange;// bool if the player is within the range of detection of the enemy
    Vector3 playerDirection;
    float angleTowardsPlayer;
    bool isShooting;
    int bulletsInClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = gameManager.instance.player.transform.position - headPos.position;// creates a vector between the player and the enemy
        if (playerInRange || playerInVisualRange())
        {
            navMesh.SetDestination(gameManager.instance.player.transform.position);// sets enemy destination as the player
            if (navMesh.remainingDistance < navMesh.stoppingDistance)// enemy is closer than nav mesh stopping distance
            {
                facePlayer();
            }

            // gun stuff
            if(!isShooting && bulletsInClip != 0)
            {
                bulletsInClip--;
                StartCoroutine(shoot());
            }else if(!isShooting && bulletsInClip == 0)// reload
            {
                StartCoroutine(gunReload());
            }
        }
    }

    /// <summary>
    /// enemy turns to face the player
    /// </summary>
    void facePlayer()
    {
        //playerDirection moved to Update
        Quaternion rot = Quaternion.LookRotation(playerDirection);// define quaternion
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
        Debug.Log(playerDistance);

        if (playerDistance < visionDistance)// player is close enough to see
        {
            angleTowardsPlayer = Vector3.Angle(headPos.position, playerDirection);
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

    IEnumerator shoot()
    {
        isShooting = true;
        GameObject bulletClone = Instantiate(bullet, headPos.position, bullet.transform.rotation);
        bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(fireRate);
        isShooting = false;

    }

    IEnumerator gunReload()
    {
        isShooting = true;
        yield return new WaitForSeconds(reloadSpeed);
        bulletsInClip = clipSize;
        isShooting = false;
    }
}
