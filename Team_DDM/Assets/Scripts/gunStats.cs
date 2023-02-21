using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    public int clipSize;
    public GameObject bullet;
    public int bulletSpeed;
    public float fireRate;
    public float reloadSpeed;
    public Transform shootPos;
}
