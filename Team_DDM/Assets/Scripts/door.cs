using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] GameObject entrance;
    [SerializeField] GameObject exit;

    public GameObject getEntrance()
    {
        return entrance;
    }
    public GameObject getExit()
    {
        return exit;
    }
}
