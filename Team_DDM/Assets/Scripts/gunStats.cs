using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    public Transform shootPos;
    public GameObject bullet;
    public GameObject gunModel;
    public int clipSize;
    public int bulletSpeed;
    public float fireRate;
    public float reloadSpeed;
}
