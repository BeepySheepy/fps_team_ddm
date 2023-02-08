using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{

    [SerializeField] Renderer enemyRenderer;
    [SerializeField] NavMeshAgent navMesh;// allows for movement
    [SerializeField] Transform headPos;// tracks head pos instead of from (0,0)
    [Range(1, 10)][SerializeField] int enemyTurnSpeed;

    [Header("-----Stats-----")]
    [SerializeField] int HP;

    bool playerInRange;// bool if the player is within the range of detection of the enemy
    Vector3 playerDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            navMesh.SetDestination(gameManager.instance.player.transform.position);// sets enemy destination as the player
            if (navMesh.remainingDistance < navMesh.stoppingDistance)// enemy is closer than nav mesh stopping distance
            {
                facePlayer();
            }
        }
    }

    /// <summary>
    /// enemy turns to face the player
    /// </summary>
    void facePlayer()
    {
        playerDirection = gameManager.instance.player.transform.position - headPos.position;// creates a vector between the player and the enemy
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
}
