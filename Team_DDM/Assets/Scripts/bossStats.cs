using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossStats : MonoBehaviour, IDamage
{
    [SerializeField] int HP;
    int hpOrig;
    [SerializeField] Renderer charModel;
    [SerializeField] GameObject gunToDrop;
    [SerializeField] GameObject ammoToDrop;
    [SerializeField] GameObject healthToDrop;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
        hpOrig = HP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// inherited takeDamage class that subtracts enemy HP
    /// </summary>
    /// <param name="dmg"></param>
    public void takeDamage(int dmg)
    {
        HP -= dmg;
        Debug.Log(this.gameObject.name + "took damage");
        StartCoroutine(flashEnemyDamage());
        if (HP <= 0)
        {
            gameManager.instance.updateGameGoal(-1);
            DropItems();
            Destroy(gameObject);// kill enemy
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

    public void updateBossHPBar()
    {
        gameManager.instance.BossHPBar.fillAmount = (float)HP / (float)hpOrig;
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