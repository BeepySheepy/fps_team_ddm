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
    [SerializeField] bool stopSpawnWhenExitSpawner;

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
}
