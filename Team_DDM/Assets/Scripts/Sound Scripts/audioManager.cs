using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] sounds[] soundArray;

    void Awake()
    {
        foreach (sounds i in soundArray)
        {
            i.soundSource = null;
        }
    }
}
