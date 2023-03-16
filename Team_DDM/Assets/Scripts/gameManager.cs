using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

enum weapons
{
    pistol = 0,
    shotgun,
    sniper
}
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
    public GameObject winLevelMenu;
    public GameObject winGameMenu;
    public GameObject loseMenu;

    [Header("---- Health ----")]
    public Image playerHPBar;
    public Image playerManaBar;
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

    [Header("---- Gun Icons ----")]
    public GameObject pistolIcon;
    public GameObject shotgunIcon;
    public GameObject sniperIcon;

    [Header("---- Level ----")]
    [SerializeField] public GameObject doorObj;
    public bool doorState;



   


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
        doorState = false;
        audio.PlayOneShot(inGameSong[Random.Range(0, inGameSong.Length)], MusicVol);
        roomCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            pauseMenu.SetActive(isPaused);
            audioManager.instance.stopByName("Ingame Song");
            audioManager.instance.playByName("Pause Song");

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
        audioManager.instance.pauseSoundEffect();
    }

    public void unPaused()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(false);
        activeMenu = null;
        audioManager.instance.resumeSoundEffect();
        audioManager.instance.stopByName("Pause Song");
        audioManager.instance.playByName("Ingame Song");
    }

    public void RoomFinished(int amount)
    {
        if (enemiesRemaining <= 0)
        {
            doorSwitch();
        }
            roomCount++;
        }
    }

    public void updateGameGoal(int amount)
    {
        BossesRemaining += amount;
        if (BossesRemaining <= 0)
        {
            paused();

            if (SceneManager.GetActiveScene().buildIndex + 1 == 0)
            {
                activeMenu = winGameMenu;
                activeMenu.SetActive(true);
            }
            else
            {
                activeMenu = winLevelMenu;
                activeMenu.SetActive(true);
            }
        }
    }

    public void gunIconIndicator(int selected)
    {
        
        switch (selected)
        {
            case 0:
                pistolIcon.SetActive(true);
                shotgunIcon.SetActive(false);
                sniperIcon.SetActive(false);
                break;
            case 1:
                pistolIcon.SetActive(false);
                shotgunIcon.SetActive(true);
                sniperIcon.SetActive(false);
                break;
            case 2:
                pistolIcon.SetActive(false);
                shotgunIcon.SetActive(false);
                sniperIcon.SetActive(true);
                break;

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

    public void doorSwitch()
    {
        doorState = !doorState;
    }
}
