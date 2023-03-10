using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    GameObject mEnemy;
    enemyAI enemyScript;

    public void SetEnemyChild(GameObject enemy)
    {
        mEnemy = enemy;
        enemyScript = mEnemy.GetComponent<enemyAI>();
    }

    private void OnDestroy()// check to see if enemy activated when spawned without particle effects
    {
        enemyScript.Activate();
    }
}
