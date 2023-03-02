using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    private void Awake()
    {
        gameManager.instance.respawnPlayer();
    }
    void Start()
    {
        gameManager.instance.respawnPlayer();
    }
}
