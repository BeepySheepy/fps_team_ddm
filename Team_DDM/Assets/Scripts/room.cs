using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class room : MonoBehaviour
{
    [SerializeField] public List<GameObject> rooms = new List<GameObject>();
    [SerializeField] public GameObject roomPos;
    [SerializeField] public GameObject boss;
    public List<GameObject> spawnedRooms = new List<GameObject>();
    Vector3 bossPos;
    public void Start()
    {
        bossPos = new Vector3(roomPos.transform.position.x, (roomPos.transform.position.y + 5.5f), roomPos.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Random rand = new Random();
            int pick = rand.Next(rooms.Count);
            GameObject roomPick = rooms[pick];
            spawnedRooms.Add(Instantiate(roomPick, roomPos.transform.position, roomPos.transform.rotation));
            gameManager.instance.roomCounter();
            if (gameManager.instance.bossSpawn())
            {
                Instantiate(boss, bossPos, roomPos.transform.rotation);
            }
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
