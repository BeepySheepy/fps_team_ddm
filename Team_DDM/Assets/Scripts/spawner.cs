using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int spawnMax;
    [SerializeField] float timer;
    [SerializeField] float spawnEffectTimer;
    [SerializeField] bool countEnemies;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] bool useSpawnEffect;
    [SerializeField] ParticleSystem spawnEffect;
    //[SerializeField] Transform[] spawnDoor;
    [SerializeField] Transform[] doorPos;
    [SerializeField] GameObject doorObj;
    [SerializeField] bool stopSpawnWhenExitSpawner;
    [SerializeField] public bool isBoss;
    [SerializeField] public bool isFinalBoss;

    [SerializeField] GameObject[] doors = new GameObject[10];
    GameObject obj;
    int spawnCount;
    bool isPlayingSpawnEffect;
    bool isSpawning;
    bool playerInRange;
    public List<GameObject> spawnList = new List<GameObject>();

    List<GameObject> enemies = new List<GameObject>();
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

        if ((spawnCount < spawnMax || spawnMax == 0) && !isSpawning && playerInRange)// spawn max = 0 equals infinite spawn
        {
            StartCoroutine(spawn());
            
        }

    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            GetComponent<Collider>().enabled = false;
            gameManager.instance.doorSwitch();
            gameManager.instance.currentRoom = this;
            //for (int i = 0; i < doorPos.Length; i++)
            //{
                //doors[i] = doorObj;
                //obj = Instantiate(doorObj, doorPos[i].transform.position, doorPos[i].transform.rotation);
                //obj.tag = "Door";
            //}
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
        yield return new WaitForSeconds(timer);

        if (useSpawnEffect)
        {
            StartCoroutine(spawnWithFX());// separate IEnumerator
        }
        else
        {
            enemies.Add(Instantiate(objectToSpawn, spawnPos[spawnCount].position, objectToSpawn.transform.rotation));
        }
        spawnCount++;

        if (spawnCount == spawnPos.Length && spawnMax == 0)
        {
            spawnCount = 0;
        }
        isSpawning = false;
    }
    IEnumerator spawnWithFX()// doesn't check for true or false because protected by spawn IEnumerator
    {
        ParticleSystem particleEffectInstance = Instantiate(spawnEffect, spawnPos[spawnCount].position, spawnEffect.transform.rotation);
        int spawnLocationInternalTimerStorage = spawnCount;// spawnCount changes outside of code
        yield return new WaitForSeconds(spawnEffectTimer);
        GameObject enemyInstance = Instantiate(objectToSpawn, spawnPos[spawnLocationInternalTimerStorage].position, objectToSpawn.transform.rotation);
        particleEffectInstance.GetComponent<spawnScript>().SetEnemyChild(enemyInstance);
    }

    public void bossKill()
    {
        if (isBoss == true && gameManager.instance.enemiesRemaining <= 0)
        {
            if (isFinalBoss)
            {
                gameManager.instance.winGame();
            }
            else
            {
                gameManager.instance.winLevel();
            }
        }
    }
    

    //public void turnOff()
    //{
    //    Destroy(gameObject);
    //}
}
