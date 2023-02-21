using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force : MonoBehaviour
{
    [SerializeField] int pushBackAmount;
    [SerializeField] bool push;
    [SerializeField] bool isConstant;
    [SerializeField] float timer;
    [SerializeField] bool negateX;
    [SerializeField] bool negateY;
    [SerializeField] bool negateZ;

    float xT, yT, zT;
    Vector3 pushCheck;

    void Start()
    {
        if (!isConstant)
        {
            Destroy(gameObject, timer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            xT = gameManager.instance.player.transform.position.x;
            yT = gameManager.instance.player.transform.position.y;
            zT = gameManager.instance.player.transform.position.z;

            if (negateX)
            {
                xT = transform.position.x;
            }
            if (negateY)
            {
                yT = transform.position.y;
            }
            if (negateZ)
            {
                zT = transform.position.z;
            }

            pushCheck = new Vector3(xT, yT, zT);

            if (!push)
            {
                gameManager.instance.playerScript.pushBackDir((transform.position - pushCheck).normalized * pushBackAmount);
            }
            else
            {
                gameManager.instance.playerScript.pushBackDir((pushCheck - transform.position).normalized * pushBackAmount);

            }
        }
    }
}
