using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    Transform localT;
    
    // Start is called before the first frame update
    void Start()
    {
        localT = GetComponent<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.updateSpawn(localT);
            Destroy(gameObject);
        }
    }
}
