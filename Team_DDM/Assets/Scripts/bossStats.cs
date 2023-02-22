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

    public GameObject BossAliveHP;

    // Start is called before the first frame update
    void Start()
    {
        BossAliveHP.SetActive(true);
        gameManager.instance.updateGameGoal(1);
        updateBossHPBar();
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
        updateBossHPBar();
        Debug.Log(this.gameObject.name + "took damage");
        StartCoroutine(flashEnemyDamage());
        if (HP <= 0)
        {
            BossAliveHP.SetActive(true);
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
