using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum enemies
{
    beeRange = 0, beeAttacker, spider, zombie, zombieSoldier, titan
}

public class enemyStats : MonoBehaviour, IDamage
{

    [SerializeField] int HP;
    int HPOrig;
    [SerializeField] Renderer charModel;
    [SerializeField] GameObject gunToDrop;
    [SerializeField] GameObject ammoToDrop;
    [SerializeField] GameObject healthToDrop;

    enemyAI aiScript;
    Animator anim;
    int enemyTypeID;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.RoomFinished(1);
        HPOrig = HP;
        anim = GetComponent<Animator>();
        aiScript = GetComponent<enemyAI>();
        enemyTypeID = aiScript.GetEnemyTypeID();
    }



    /// <summary>
    /// inherited takeDamage class that subtracts enemy HP
    /// </summary>
    /// <param name="dmg"></param>
    public void takeDamage(int dmg)
    {
        HP -= dmg;
        Debug.Log(this.gameObject.name + "took damage");
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
            }
        }
        else
        {
            anim.SetTrigger("Hit");
            StartCoroutine(flashEnemyDamage());
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


}
