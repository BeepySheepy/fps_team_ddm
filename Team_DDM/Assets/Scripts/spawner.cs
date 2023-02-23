using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int spawnMax;
    [SerializeField] float timer;
    [SerializeField] bool countEnemies;
    [SerializeField] Transform[] spawnPos;
    //[SerializeField] Transform[] spawnDoor;
    [SerializeField] Transform[] doorPos;
    [SerializeField] GameObject doorObj;
    [SerializeField] bool stopSpawnWhenExitSpawner;

    [SerializeField] GameObject[] doors = new GameObject[10];
    GameObject obj;
    int spawnCount;
    bool isSpawning;
    bool playerInRange;
    List<GameObject> spawnList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (objectToSpawn.CompareTag("Enemy") && countEnemies)// checks if the spawned objects are enemies and whether they should be counted
        {
            gameManager.instance.updateGameGoal(spawnMax);
        }
        //doorObj = GetComponent<door>().getDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
        if((spawnCount < spawnMax || spawnMax == 0) && !isSpawning && playerInRange)// spawn max = 0 equals infinite spawn
        {
            StartCoroutine(spawn());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            for (int i = 0; i < doorPos.Length; i++)
            {
                //doors[i] = doorObj;
                obj = Instantiate(doorObj, doorPos[i].transform.position, doorPos[i].transform.rotation);
                obj.tag = "Door";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && stopSpawnWhenExitSpawner)
        {
            playerInRange = false;
        }
    }

    IEnumerator spawn()
    {
        isSpawning = true;
        Instantiate(objectToSpawn, spawnPos[spawnCount].position, objectToSpawn.transform.rotation);
        spawnCount++;
        
        if(spawnCount == spawnPos.Length && spawnMax == 0)
        {
            spawnCount = 0;
        }
        yield return new WaitForSeconds(timer);
        isSpawning = false;
    }

    //public void turnOff()
    //{
    //    Destroy(gameObject);
    //}
}
