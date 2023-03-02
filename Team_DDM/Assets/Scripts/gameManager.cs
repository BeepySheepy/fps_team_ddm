using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class gameManager : MonoBehaviour
{

    public static gameManager instance;// instance of game manager class

    [Header("---- Player ----")]
    public GameObject player;// player object accesible thorugh other classes
    public playerController playerScript;
    public GameObject playerSpawn;

    [Header("---- Menus ----")]
    public GameObject pauseMenu;
    public GameObject activeMenu;
    public bool isPaused;
    public GameObject winMenu;
    public GameObject loseMenu;

    [Header("---- Health ----")]
    public Image playerHPBar;
    public Image BossHPBar;
    public GameObject playerDamageFlasher;
    public GameObject playerHealFlasher;

    [Header("--- Enemies ----")]
    public int enemiesRemaining;
    [SerializeField] TextMeshProUGUI enemiesRemainingText;

    [Header("---- Ammo ----")]
    [SerializeField] TextMeshProUGUI ammoEconomyF;
    [SerializeField] TextMeshProUGUI ammoEconomyI;

    [Header("---- Gun Spawns ----")]
    [SerializeField] GameObject shotgunSpot;
    [SerializeField] GameObject shotgunPickup;
    [SerializeField] GameObject sniperSpot;
    [SerializeField] GameObject sniperPickup;
    public GameObject Realoading;
    int gunSpawn;
    public int BossesRemaining;

    GameObject levelSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
        shotgunSpot = GameObject.Find("ShotgunSpawn");
        shotgunPickup = GameObject.Find("Gun - Shotgun");
        sniperSpot = GameObject.Find("SniperSpawn");
        sniperPickup = GameObject.Find("Gun - Sniper");
        gunSpawn = 0;
        playerSpawn = GameObject.FindGameObjectWithTag("Respawn");
        //Debug.Log($"Spawn:  + {playerSpawn.transform.position.x}, {playerSpawn.transform.position.y}, {playerSpawn.transform.position.z}");
        levelSpawn = playerSpawn;
        respawnPlayer();
    }

    private void Start()
    {
        respawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            pauseMenu.SetActive(isPaused);

            if (isPaused)
            {
                paused();
            }
            else
            {
                unPaused();
            }

        }
    }

    public void paused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void unPaused()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void RoomFinished(int amount)
    {
        enemiesRemaining += amount;
        enemiesRemainingText.text = enemiesRemaining.ToString("F0");
        if (enemiesRemaining <= 0)
        {
            gunSpawn++;
            //Debug.Log("Should end.");
            //for (int i = 0; i < spawn.getDoors().Length; i++)
            //{
            //    //Destroy(spawn.getDoors()[i]);
            //    //spawn.getDoors()[i].GetComponent<door>().turnOff();
            //}
            //Destroy(GameObject.FindWithTag("Door"));
            //Destroy(GameObject.FindWithTag("Spawner"));
            shotgunSpot = GameObject.Find("ShotgunSpawn");
            shotgunPickup = GameObject.Find("Gun - Shotgun");
            sniperSpot = GameObject.Find("SniperSpawn");
            sniperPickup = GameObject.Find("Gun - Sniper");
            if (gunSpawn == 1)
            {
                Instantiate(shotgunPickup, shotgunSpot.transform.position, shotgunSpot.transform.rotation);

            }
            else if (gunSpawn == 2)
            {
                Instantiate(sniperPickup, sniperSpot.transform.position, sniperSpot.transform.rotation);

            }
            else
            {
                updateGameGoal(-10);
            }
        }
    }

    public void updateGameGoal(int amount)
    {
        BossesRemaining += amount;
        if (BossesRemaining <= 0)
        {
            paused();
            activeMenu = winMenu;
            activeMenu.SetActive(true);
        }
    }
    

    public void ammoUpdaterF(int amount)
    {
        ammoEconomyF.text = amount.ToString("F0");
    }

    public void ammoUpdaterI(int amount)
    {
        ammoEconomyI.text = amount.ToString("F0");
    }

    public void reloadDisplay(bool reloading)
    {
        if (reloading == true)
        {
            Realoading.SetActive(true);
        }
        else
        {
            Realoading.SetActive(false);
        }
    }

    public void playerDead()
    {
        paused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }
    public void respawnPlayer()
    {
        player.transform.position = new Vector3(playerSpawn.transform.position.x, playerSpawn.transform.position.y, playerSpawn.transform.position.z);
        Debug.Log($"Respawned at:  + {player.transform.position.x}, {player.transform.position.y}, {player.transform.position.z}");
    }
    public void updateSpawn(Transform newSpawn)
    {
        playerSpawn.transform.position = new Vector3(newSpawn.transform.position.x, newSpawn.transform.position.y, newSpawn.transform.position.z);
        Debug.Log($"New Spawn:  + {playerSpawn.transform.position.x}, {playerSpawn.transform.position.y}, {playerSpawn.transform.position.z}");
    }
}
