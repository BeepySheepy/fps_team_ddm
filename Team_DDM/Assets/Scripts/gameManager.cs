using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public static gameManager instance;// instance of game manager class

    [Header("---- Player ----")]
    public GameObject player;// player object accesible thorugh other classes
    public playerController playerScript;
    public GameObject playerSpawn;
    public spawner currentRoom;

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
    public GameObject playerShieldFlasher;
    public GameObject playerShieldIcon;

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

    int songPlay;

   


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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            pauseMenu.SetActive(isPaused);
            songPlay = 1;

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
        audioManager.instance.stopByName("Ingame Song");
        switch (songPlay)
        {
            case 1:
                audioManager.instance.playByName("Pause Song");
                break;
            case 2:
                audioManager.instance.playByName("Win Song");
                break;
            case 3:
                audioManager.instance.playByName("Lose Song");
                break;

        }
    }

    public void unPaused()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(false);
        activeMenu = null;
        audioManager.instance.pauseSoundEffect();
        audioManager.instance.playByName("Ingame Song");
        switch (songPlay)
        {
            case 1:
                audioManager.instance.stopByName("Pause Song");
                break;                
            case 2:                   
                audioManager.instance.stopByName("Win Song");
                break;                
            case 3:                   
                audioManager.instance.stopByName("Lose Song");
                break;

        }
    }

    public void RoomFinished(int amount)
    {
        enemiesRemaining += amount;
        enemiesRemainingText.text = enemiesRemaining.ToString("F0");
        if (enemiesRemaining <= 0)
        {
            doorSwitch();
        }
    }

    public void updateGameGoal(int amount)
    {
        BossesRemaining += amount;
        if (BossesRemaining <= 0)
        {
            songPlay = 2;
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

    public void winLevel()
    {
        songPlay = 2;
        paused();
        activeMenu = winLevelMenu;
        activeMenu.SetActive(true);
    }

    public void winGame()
    {
        songPlay = 2;
        paused();
        activeMenu = winGameMenu;
        activeMenu.SetActive(true);
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
    public IEnumerator shielded()
    {
        int i = 0;
        playerShieldFlasher.SetActive(true);
        yield return new WaitForSeconds(1);
        playerShieldFlasher.SetActive(false);
    }
    public void shieldedIcon(bool God)
    {
        if (God == true)
        {
            playerShieldIcon.SetActive(true);
        }
        else
        {
            playerShieldIcon.SetActive(false);
        }
    }

    public void playerDead()
    {
        songPlay = 3;
        paused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }

    public void doorSwitch()
    {
        doorState = !doorState;
    }
}
