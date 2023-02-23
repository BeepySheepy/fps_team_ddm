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

    [Header("--- Enemies ----")]
    public int enemiesRemaining;
    [SerializeField] TextMeshProUGUI enemiesRemainingText;

    [Header("---- Ammo ----")]
    [SerializeField] TextMeshProUGUI ammoEconomyF;
    [SerializeField] TextMeshProUGUI ammoEconomyI;
    public GameObject Realoading;
    int gunSelected;
    public int BossesRemaining;

    spawner spawn;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
        spawn = GetComponent<spawner>();
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
            //Debug.Log("Should end.");
            //for (int i = 0; i < spawn.getDoors().Length; i++)
            //{
            //    //Destroy(spawn.getDoors()[i]);
            //    //spawn.getDoors()[i].GetComponent<door>().turnOff();
            //}
            Debug.Log("Doors About to be Destroyed!");
            Destroy(GameObject.FindWithTag("Door"));
            Debug.Log("Doors Destroyed!");
            Destroy(GameObject.FindWithTag("Spawner"));
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
}
