using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossKill : MonoBehaviour
{
    [SerializeField] int bossHP;
    GameObject enemy;
    private void Start()
    {
        
    }

    void Update()
    {
        //bossHP = 
        if (bossHP <= 0)
        {
            gameManager.instance.winLevel();
        }
    }
}
