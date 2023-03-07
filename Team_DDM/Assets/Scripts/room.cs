using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class room : MonoBehaviour
{
    [SerializeField] public List<GameObject> rooms = new List<GameObject>();
    [SerializeField] public GameObject roomPos;
    public List<GameObject> spawnedRooms = new List<GameObject>();
    public int roomCount;

    private void Awake()
    {
        roomCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Random rand = new Random();
            int pick = rand.Next(rooms.Count);
            GameObject roomPick = rooms[pick];
            spawnedRooms.Add(Instantiate(roomPick, roomPos.transform.position, roomPos.transform.rotation));
            roomCount++;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
