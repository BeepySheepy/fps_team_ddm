using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum enemies
{
    basicRange = 0, basicMelee, bulletHell, boss, spider
}// remove spider code eventually

public class enemyStats : MonoBehaviour, IDamage
{

    [SerializeField] int HP;
    int HPOrig;
    [SerializeField] Renderer charModel;
    [SerializeField] GameObject gunToDrop;
    [SerializeField] GameObject ammoToDrop;
    [SerializeField] GameObject healthToDrop;
    [SerializeField] float enemyInvTimer;

    enemyAI aiScript;
    Animator anim;
    int enemyTypeID;
    CapsuleCollider collider;
    bool isInv = false;
    bool phase1, phase2, phase3 = false;


    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.RoomFinished(1);
        HPOrig = HP;
        anim = GetComponent<Animator>();
        aiScript = GetComponent<enemyAI>();
        enemyTypeID = aiScript.GetEnemyTypeID();
        collider = GetComponent<CapsuleCollider>();
        if (enemyTypeID == (int)enemies.boss)// start boss phase at beginning of fight
        {
            EnemyBossPhaseEnter();
        }
    }



    /// <summary>
    /// inherited takeDamage class that subtracts enemy HP
    /// </summary>
    /// <param name="dmg"></param>
    public void takeDamage(int dmg)
    {
        if (!isInv)// not invincible
        { 
            HP -= dmg;
            Debug.Log(this.gameObject.name + "took damage");
            //aiScript.GetHeadPos().gameObject.GetComponent<Collider>().enabled = false;
            if (HP <= 0)
            {
                gameManager.instance.RoomFinished(-1);
                DropItems();
                if ((enemies)enemyTypeID == enemies.spider)
                {
                    Destroy(gameObject);// kill enemy
                }
                else
                {
                    anim.SetBool("Dead", true);
                    aiScript.TurnOffNavMesh();
                    collider.enabled = false;
                }
            }
            else
            {
                anim.SetTrigger("Hit");
                StartCoroutine(flashEnemyDamage());
            }

            if (enemyTypeID == (int)enemies.boss)// run enemy boss functions
            {
                EnemyBossPhaseEnter();
            }
        }

    }

    /// <summary>
    /// flashes the enemy red
    /// </summary>
    /// <returns></returns>
    IEnumerator flashEnemyDamage()
    {
        Color modelColor = charModel.material.color;
        charModel.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        charModel.material.color = modelColor;
    }

    void DropItems()
    {
        if (gunToDrop != null)
        {
            Instantiate(gunToDrop, transform.position, transform.rotation);
        }
        if (ammoToDrop != null)
        {
            Instantiate(ammoToDrop, transform.position + new Vector3(1, 1, 0), transform.rotation);
        }
        if (healthToDrop != null)
        {
            Instantiate(healthToDrop, transform.position + new Vector3(-1, 1, 0), transform.rotation);
        }
    }

    /// <summary>
    /// checks the enemy health and has it enter the selected enemy boss phase
    /// </summary>
    void EnemyBossPhaseEnter()
    {
        if (HP > (HPOrig * .7f) && !phase1)// phase 1(melee)
        {
            phase1 = true;
            StartCoroutine(InvincibilityFrames());
            anim.SetTrigger("Phase 1");
        }
        else if (HP > (HPOrig * .5f) && !phase2)// phase 2(single shot)
        {
            phase2 = true;
            StartCoroutine(InvincibilityFrames());
            anim.SetTrigger("Phase 2");
        }
        else if (!phase3)// phase 3(bullet hell boss)
        {
            phase3 = true;
            StartCoroutine(InvincibilityFrames());
            anim.SetTrigger("Phase 3");
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInv = true;
        yield return new WaitForSeconds(enemyInvTimer);
        isInv = false;
    }

}
