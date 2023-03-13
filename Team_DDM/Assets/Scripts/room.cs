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
    public List<GameObject> doors = new List<GameObject>();
    bool roomActive;
    public void Start()
    {
        roomActive = false;
    }
    public void Update()
    {
        if (roomActive == true && gameManager.instance.enemiesRemaining <= 0)
        {
            RoomDeactivate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomActive = true;
            GetComponent<BoxCollider>().enabled = false;
            RoomActivate();
        }
    }

    private void RoomActivate()
    {
        Random rand = new Random();
        int pick = rand.Next(rooms.Count);
        GameObject roomPick = rooms[pick];
        spawnedRooms.Add(Instantiate(roomPick, roomPos.transform.position, roomPos.transform.rotation));
        gameManager.instance.roomCounter();
        if (gameManager.instance.bossSpawn())
        {
            //Instantiate(boss, bossPos, roomPos.transform.rotation);
        }
        gameManager.instance.doorSwitch();
    }
    private void RoomDeactivate()
    {
        gameManager.instance.doorSwitch();
    }
}
