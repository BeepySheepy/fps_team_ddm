using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] GameObject doorBod;
    bool byebye;
    // Start is called before the first frame update
    void Start()
    {
        byebye = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (byebye)
        {
            Destroy(gameObject);
        }

    }

    public void turnOff()
    {
        byebye = true;
    }
    public GameObject getDoor()
    {
        return doorBod;
    }
}
